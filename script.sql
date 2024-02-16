CREATE TABLE "Clientes" (
    "Id" SERIAL PRIMARY KEY,
    "Limite" bigint NOT NULL,
    "Saldo" bigint NOT NULL,
    "Nome" VARCHAR(50)
);
CREATE TABLE "Transacoes" (
    "Id" SERIAL PRIMARY KEY,
    "Valor" bigint NOT NULL,
    "Tipo" char NOT NULL,
    "Descricao" VARCHAR(10) NOT NULL,
    "RealizadaEm" date NOT NULL,
    "ClienteId" int NOT NULL,
    CONSTRAINT "FK_Transacoes_Clientes_ClienteId" FOREIGN KEY ("ClienteId") REFERENCES "Clientes" ("Id")
);

CREATE INDEX index_idCliente_transacoes ON "Transacoes"("ClienteId" ASC);
DO $$
BEGIN
  INSERT INTO "Clientes" ("Nome", "Limite", "Saldo")
  VALUES
    ('o barato sai caro', 1000 * 100,0),
    ('zan corp ltda', 800 * 100,0),
    ('les cruders', 10000 * 100,0),
    ('padaria joia de cocaia', 100000 * 100,0),
    ('kid mais', 5000 * 100,0);
END; $$
