export interface RespuestaPaginada<T> {
    pageNumber: number;
    pageSize: number;
    totalRecords: number;
    totalPages: number;
    datos: T[];
    succeeded: boolean;
    mensaje: string | null;
    errores: string[] | null;
}

export interface Respuesta<T> {
    succeeded: boolean;
    mensaje: string;
    errores: string[];
    datos: T;
}
