using apiSipinna.Models;

namespace apiSipinna.CRUD;

public interface IOperations{

    /// <summary>
    /// Función para añadir una fila en la tabla correspondiente, en la base de
    /// datos, al objeto pasado como argumento. EntityFramework se encarga de
    /// ubicar donde se almacenará el objeto.
    /// </summary>
    /// <param name="obj">El objeto que se desea agregar a la base de datos.</param>
    /// <returns>true en caso de tener éxito al registrar el objeto en la base de datos, false en caso contrario</returns>
    public Task<bool> Create(object obj);

    #region Categoria

    /// <summary>
    /// Lee una fila de la tabla categoría en la base de datos dado un id
    /// </summary>
    /// <param name="id">id de la fila que se desea leer de la tabla 'Categoria' en la base de datos</param>
    /// <returns>Regresa una instancia del objecto Categoria con el id específicado.</returns>
    public Task<Categoria> ReadCategoria(int id);

    public Task<Boolean> UpdateCategoria(Categoria cat);
    public Task<Boolean> DeleteCategoria(int id);

    #endregion

    #region Cobertura
    public Task<Categoria> ReadCobertura(int id);
    public Task<Boolean> UpdateCobertura(Categoria cat);
    public Task<Boolean> DeleteCobertura(int id);
    #endregion

    #region Edades
        public Task<Edades> ReadEdades(int id);
        public Task<Boolean> UpdateEdades(Edades edades);
        public Task<Boolean> DeleteEdades(int id);
    #endregion

    #region Estadistica
        public Task<Estadistica> ReadEstadistica(int id);
        public Task<Boolean> UpdateEstadistica(Estadistica estadistica);
        public Task<Boolean> DeleteEstadistica(int id);
    #endregion

    #region Fecha
        public Task<Fecha> ReadFecha(int id);
        public Task<Boolean> UpdateFecha(Fecha fecha);
        public Task<Boolean> DeleteFecha(int id);
        public Task<Boolean> DeleteFecha(Fecha fecha);
    #endregion

    #region Lugar
        public Task<Lugar> ReadLugar(int id);
        public Task<Boolean> UpdateLugar(Lugar lugar);
        public Task<Boolean> DeleteLugar(Lugar lugar);
        public Task<Boolean> DeleteLugar(int id);

    #endregion

}