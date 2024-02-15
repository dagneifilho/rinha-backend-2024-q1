CREATE OR REPLACE FUNCTION realizar_transacao(
    p_id int,
    p_valor bigint,
    p_tipo char,
    p_descricao text,
    p_realizadaEm date
) RETURNS RECORD AS $$
DECLARE 
  limite bigint;
  saldo bigint;
  novo_saldo bigint;
  novo_limite bigint;
  resultado RECORD;
BEGIN 
  -- Travar a tabela "Clientes" em modo exclusivo de linha
  LOCK TABLE "Clientes" IN ROW EXCLUSIVE MODE;
  
  -- Selecionar e travar a linha específica para atualização
  SELECT "Limite", "Saldo" INTO limite, saldo 
  FROM "Clientes" c 
  WHERE "Id" = p_id 
  FOR UPDATE;


  -- Atualizar o saldo com base no tipo
  IF (p_tipo = 'd') THEN
  	IF ((-p_valor + saldo) < -limite) THEN 
    	RAISE EXCEPTION 'limite insuficiente';
 	END IF;
    UPDATE "Clientes" SET "Saldo" = saldo - p_valor WHERE "Id" = p_id	
    RETURNING "Saldo" AS novo_saldo, "Limite" AS novo_limite INTO novo_saldo, novo_limite;
  ELSE
    UPDATE "Clientes" SET "Saldo" = saldo + p_valor WHERE "Id" = p_id
    RETURNING "Saldo" AS novo_saldo, "Limite" AS novo_limite INTO novo_saldo, novo_limite;
  END IF;

  -- Inserir na tabela de transações
  INSERT INTO "Transacoes" ("Tipo", "Valor", "Descricao", "RealizadaEm", "ClienteId") 
  VALUES (p_tipo, p_valor, p_descricao, p_realizadaEm, p_id);

  -- Retornar um record com os valores atualizados
  resultado := (novo_saldo, novo_limite);
  RETURN resultado;
END $$ LANGUAGE plpgsql;
