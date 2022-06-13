create table if not exists facts (
    uid serial primary key,
    "desc" text NOT NULL,
    "from" timestamp NOT NULL,
    "to" timestamp NOT NULL,
    check("from" <= "to"),
    delta interval generated always as ("to" - "from") stored,
    props json
);

create table if not exists regions (
    uid serial primary key,
    pretty_name text NOT NULL,
    "polygon" polygon NOT NULL
);

create table if not exists facts_regions_n2n (
    fact_uid int references facts(uid) on delete restrict,
    region_uid int references regions(uid) on delete cascade,
    primary key(fact_uid, region_uid)
);