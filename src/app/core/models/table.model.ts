export interface ITable {
    idestadistica: number;
    cobertura:     number;
    coberturaNav:  CoberturaNav;
    categoria:     number;
    categoriaNav:  CategoriaNav;
    edades:        number;
    edadesNav:     EdadesNav;
    lugar:         number;
    lugarNav:      LugarNav;
    fecha:         number;
    fechaNav:      FechaNav;
    dato:          number;
}

export interface CategoriaNav {
    idCategoria: number;
    dominio:     Dominio;
    categoria:   Categoria;
    indicador:   Indicador;
}

export enum Categoria {
    Trabajo = "Trabajo",
}

export enum Dominio {
    Proteccion = "Proteccion",
}

export enum Indicador {
    PromedioDeHorasQueTrabajanLasNiñasNiñosYAdolescentes = "Promedio de horas que trabajan las niñas, niños y adolescentes",
}

export interface CoberturaNav {
    idCobertura: number;
    alcance:     Alcance;
    poblacion:   Poblacion;
}

export enum Alcance {
    Nacional = "Nacional",
}

export enum Poblacion {
    Hombre = "Hombre",
    Mujer = "Mujer",
    Total = "Total",
}

export interface EdadesNav {
    idedades:    number;
    rangoEdades: RangoEdades;
}

export enum RangoEdades {
    The57 = "5-7",
}

export interface FechaNav {
    idfecha: number;
    anio:    number;
    mes:     Mes;
}

export enum Mes {
    Febrero = "Febrero",
}

export interface LugarNav {
    idLugar: number;
    entidad: string;
}
