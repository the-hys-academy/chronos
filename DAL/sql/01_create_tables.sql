
create table if not exists facts (
    id bigserial primary key,
    "desc" text NOT NULL,
    "from" timestamp NOT NULL,
    "to" timestamp NOT NULL,
    check("from" <= "to"),
    delta interval generated always as ("to" - "from") stored,
    props json
);

create table if not exists regions (
    id bigserial primary key,
    pretty_name text NOT NULL,
    "polygon" polygon NOT NULL
);

create table if not exists facts_regions_n2n (
    fact_id int references facts(id) on delete restrict,
    region_id int references regions(id) on delete cascade,
    primary key(fact_id, region_id)
);
