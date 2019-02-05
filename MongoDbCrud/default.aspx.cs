using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace MongoDbCrud
{
    public partial class _default : System.Web.UI.Page
    {
        #region Variables Globales | Conexion MongoDB

        // Se establece la cadena de conexion de mongo donde se encuentra la BD
        protected static MongoClient client = new MongoClient("mongodb://MarcoBrito:Colombia.2019@localhost:27017/Capacitacion");

        // se establece la base de datos donde se realizaran las operaciones
        protected static IMongoDatabase database = client.GetDatabase("Capacitacion");

        #endregion


        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            //Al iniciar la pagina por primera vez, se limpia todo
            if (!IsPostBack)
            {
                LimpiarFormulario();
            }

        }

        protected void btnInsertar_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            usuario.nombre = txtNombre.Text;
            usuario.apellido = txtApellido.Text;
            usuario.cargo = txtCargo.Text;
            usuario.img = "dw.jpg";

            Insertar(usuario);

        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            Editar();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            //Limpiamos todo
            LimpiarFormulario();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string buscador = txtId.Text;

            //Valido si el campo esta vacio
            if (buscador.Length == 0)
            {
                lblResultado.Text = "Debe ingresar un ID para consultar";
            }
            else
            {
                //No muestro nada o elimino la info
                lblResultado.Text = string.Empty;
                Buscar(buscador);
            }
        }

        /*
        protected void txtId_TextChanged(object sender, EventArgs e)
        {
            // txtId.Focus();
            //txtNombre.Text = "se viene";

            if (txtId.IsFocused)
            {
                txtNombre.Text = "se viene";
            }
        }
        */

        #endregion


        #region Metodos privados

        private void Buscar(string busqueda) {

            //Se obtiene la coleccion en este caso USUARIO
            var colleccion = database.GetCollection<BsonDocument>("USUARIO");

            //Se hace uso de TryParse para ver si la conversión a entero del texto es posible. 
            //Si lo es,es un valor numérico. Si no, es un texto
            int temp = 0;
            if (!int.TryParse(busqueda, out temp)) {

                //Se obtiene el filtro, es decir la condicion WHERE, por el id del USUARIO
                //Como el ID es generado por el motor de BD, se debe convertir al tipo de datos para su ejecucion


                var filtro = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(busqueda));
 
                //Se realiza la consulta
                var resultado = colleccion.Find(filtro).FirstOrDefault();

                //Si el resultado es nulo es porque la consulta no arrojo resultado, en caso contrario se carga toda la info

                if (resultado != null)
                {
                    txtNombre.Text = resultado["nombre"] != null ? resultado["nombre"].ToString() : string.Empty;
                    txtApellido.Text = resultado["apellido"] != null ? resultado["apellido"].ToString() : string.Empty;
                    txtCargo.Text = resultado["cargo"] != null ? resultado["cargo"].ToString() : string.Empty;
                    foto.Src = resultado["img"] != null ? "./img/" + resultado["img"] : "./img/dw.jpg";


                    // para hacerlo dinamico y simplificar codigo se crea un metodo que administre los estado de los botones
                    EstablecerEdicion();

                }
                else
                {

                    lblResultado.Text = "No se encontro el usuario con el ID " + busqueda;
                    //Reseteamos todo
                    LimpiarFormulario();

                }

            }
            else
            {
                // Busco en otro campo de cedula a ver que talco.... XD
                var filtro = Builders<BsonDocument>.Filter.Eq("cedula", busqueda);
                var resultado = colleccion.Find(filtro).FirstOrDefault();

                if (resultado != null)
                {
                    txtNombre.Text = resultado["nombre"] != null ? resultado["nombre"].ToString() : string.Empty;
                    txtApellido.Text = resultado["apellido"] != null ? resultado["apellido"].ToString() : string.Empty;
                    txtCargo.Text = resultado["cargo"] != null ? resultado["cargo"].ToString() : string.Empty;
                    foto.Src = resultado["img"] != null ? "./img/" + resultado["img"] : "./img/dw.jpg";

                    // para hacerlo dinamico y simplificar codigo se crea un metodo que administre los estado de los botones
                    EstablecerEdicion();

                }
                else
                {

                    lblResultado.Text = "No se encontro el usuario con la cedula " + busqueda;
                    //Reseteamos todo
                    LimpiarFormulario();

                }

            }
   
        }

        private void Insertar(Usuario usuario) {
            var colleccion = database.GetCollection<BsonDocument>("USUARIO");
            // se crea el documento a partir de la clase
            BsonDocument documento = usuario.ToBsonDocument();

            //se crea el documento en la coleccion
            colleccion.InsertOne(documento);

            lblResultadoExitoso.Text = "Datos guardados exitosamente!";

            /*
            if (colleccion.InsertOne(documento)) {
                lblResultadoExitoso.Text = "Datos guardados exitosamente!";
            } else {
                lblResultado.Text = "Error insertando los datos";
            }
            */

            //Al insertar se crea el ID y lo recuperamos
            txtId.Text = documento["_id"].ToString();
            
            //activamos la botonera
            EstablecerEdicion();
        }

        private void Editar() {

            var colleccion = database.GetCollection<BsonDocument>("USUARIO");
            // se establece el filtro la condicion where
            var filtro = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(txtId.Text));

            // llamamos al modelo para editarlo
            Usuario usuario = new Usuario();
            usuario._id = ObjectId.Parse(txtId.Text);
            usuario.nombre = txtNombre.Text;
            usuario.apellido = txtApellido.Text;
            usuario.cargo = txtCargo.Text;
            usuario.img = "dw.jpg";

            colleccion.ReplaceOne(filtro, usuario.ToBsonDocument());
            lblResultadoExitoso.Text = "Datos modificados correctamente!";
            /*
            if (colleccion.ReplaceOne(filtro, usuario.ToBsonDocument())) {
                lblResultadoExitoso.Text = "Datos modificados correctamente!";
            } else {
                lblResultado.Text = "Error modificando los datos";
            }*/
        }

        private void Eliminar() {
            var colleccion = database.GetCollection<BsonDocument>("USUARIO");
            // se establece el filtro la condicion where
            var filtro = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(txtId.Text));

            //Se elimina el documento asociado al filtro
            colleccion.DeleteOne(filtro);
            lblResultado.Text = "Datos eliminados correctamente!";
            /*
            if (colleccion.DeleteOne(filtro)){
                lblResultadoExitoso.Text = "Datos eliminados correctamente!";
            } else {
                lblResultado.Text = "Error eliminando los datos";
            }*/

            //al borrarlo limpiamos el formulario
            LimpiarFormulario();
        }

        private void LimpiarFormulario() {
            foto.Src = "./img/dw.jpg";
            lblResultadoExitoso.Text = string.Empty;
            txtId.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtCargo.Text = string.Empty;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            btnInsertar.Enabled = true;
            btnBuscar.Enabled = true;
        }

        private void EstablecerEdicion() {
            lblResultado.Text = string.Empty;
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
            btnInsertar.Enabled = false;
            btnBuscar.Enabled = false;
        }



        #endregion


        #region Clases y Modelos

        private class Usuario
        {
            [BsonId]
            public ObjectId _id { get; set; }

            public string nombre { get; set; }
            public string apellido { get; set; }
            public string cargo { get; set; }
            public string img { get; set; }


        }

        #endregion


    }
}