using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Platform.Entity.Entity;

namespace Platform.Entity.DAO
{
    public class daoDirector
    {

        EntityEntities d;
        int idDirector;
        List<Proyecto> proyectos;
        Proyecto pro;
        List<Usuario> usuario;
        Usuario u;

        public daoDirector()
        {
            d = new EntityEntities();
            proyectos = new List<Proyecto>();
            pro = new Proyecto();
            usuario = new List<Usuario>();
            u = new Usuario();
        }

        
        /**
         * Metodo para conocer el id del usuario conectado
         **/
        public int proyectosDirector(String usuario)
        {

            var consulta = d.Usuario.Where(u => u.usuario1 == usuario).
                Select(u => new { u.id }).ToList();

            int cantidadRegistrosCons = consulta.Count;

            if(cantidadRegistrosCons != 0){
                consulta.First();

                foreach (var usu in consulta)
                {
                    idDirector = usu.id;
                }

                return idDirector;
            }
            else
            {
                return 0;
            }

            

        }

        /**
         * Metodo para saber si hay registro con el usuario conectado 
         * */
        public bool verificarUsuario(String usuario)
        {

            var consulta = d.Usuario.Where(u => u.usuario1 == usuario).
                Select(u => new { u.id }).ToList();

            int i = consulta.Count;

            if (i != 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        /**
         * Metodo para traer los proyectos de un director
         * */
        public List<Proyecto> listaProyectos(int idUsu){

            var consulta = d.Proyecto.Where(p => p.Usuario_id == idUsu).
                 Select(p => new { p.nombre , p.fecha_inicio, p.fecha_fin,
                 p.etapa,p.Usuario_id}).ToList();

            foreach(var p in consulta){
                pro.nombre = p.nombre;
                pro.fecha_inicio = p.fecha_inicio;
                pro.fecha_fin = p.fecha_fin;
                pro.etapa = p.etapa;                
                pro.Usuario_id = p.Usuario_id;
                proyectos.Add(pro);
            }
            
            return proyectos;
        }

        /**
         * Metodo para traer los datos del usuario conectado
         * */
        public List<Usuario> listarUsu(String usu)
        {
            var consulta = d.Usuario.Where(p => p.usuario1 == usu).
                 Select(p => new
                 {
                     p.cedula,
                     p.nombre,
                     p.apellido,
                     p.edad,
                     p.telefono,
                     p.usuario1,
                     p.contrasenia,
                     p.Tipo_Usuario,
                     p.correo
                 }).ToList();

            foreach (var p in consulta)
            {
                u.cedula = p.cedula;
                u.nombre = p.nombre;
                u.apellido = p.apellido;
                u.edad = p.edad;
                u.telefono = p.telefono;
                u.usuario1 = p.usuario1;
                u.contrasenia = p.contrasenia;
                u.Tipo_Usuario = p.Tipo_Usuario;
                u.correo = p.correo;
                usuario.Add(u);
            }

            return usuario;
        }

    }
    
}
