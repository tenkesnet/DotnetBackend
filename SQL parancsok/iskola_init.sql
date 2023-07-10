CREATE TABLE "lakcim" (
	"id"	INTEGER,
	"telepules"	TEXT,
	"irszam"	INTEGER,
	"utca"	TEXT,
	"hazszam"	INTEGER,
	PRIMARY KEY("id" AUTOINCREMENT)
)
CREATE TABLE "tanarok" (
	"id"	INTEGER,
	"name"	TEXT,
	"szuldatum"	date,
	"nem"	TEXT,
	"fotantargy"	TEXT,
	"lakcim_id"	INTEGER,
	PRIMARY KEY("id" AUTOINCREMENT)
)
CREATE TABLE "tanulok" (
	"id"	integer,
	"name"	text,
	"szuldatum"	date,
	"nem"	text,
	"tanatlag"	double,
	"lakcim_id"	INTEGER,
	PRIMARY KEY("id" AUTOINCREMENT)
)
