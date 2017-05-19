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
        List<Cargo> cargos;
        Proyecto pro;
        Cargo cargo;
        List<Usuario> usuario;
        Usuario u;
        List<Integrante> integrantes;
        Integrante integrante;
        List<Actividad> actividades;
        Actividad actividad;
        private EntityEntities db = new EntityEntities();

        public daoDirector()
        {
            d = new EntityEntities();
            proyectos = new List<Proyecto>();
            pro = new Proyecto();
            cargos = new List<Cargo>();            
            usuario = new List<Usuario>();
            u = new Usuario();
            integrantes = new List<Integrante>();
            actividades = new List<Actividad>();
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
                 Select(p => new
                 {
                     p.id,
                     p.nombre,
                     p.fecha_inicio,
                     p.fecha_fin,
                     p.etapa,
                     p.Usuario_id
                 }).ToList();

            foreach(var p in consulta){
                pro.id = p.id;
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
        * Metodo para traer los cargos de un proyecto
        * */
        public List<Cargo> listaCargos(int idPro)
        {

            var consulta = d.Cargo.Where(p => p.Proyecto_id == idPro).
                 Select(p => new
                 {
                     p.id,
                     p.nombre,
                     p.salario,
                     p.horario,
                     p.Proyecto_id
                 }).ToList();

            foreach (var p in consulta)
            {
                cargo = new Cargo();
                cargo.id = p.id;
                cargo.nombre = p.nombre;
                cargo.salario = p.salario;
                cargo.horario = p.horario;
                cargo.Proyecto_id = p.Proyecto_id;
                cargos.Add(cargo);
            }

            return cargos;
        }


        /**
        * Metodo para traer las actividades de un proyecto
        * */
        public List<Actividad> listaActividad(int idPro)
        {

            var consulta = d.Actividad.Where(p => p.Proyecto_id == idPro).
                 Select(p => new
                 {
                     p.id,
                     p.nombre,
                     p.fecha_inicio,
                     p.fecha_fin,
                     p.descripcion,
                     p.Proyecto_id,
                     p.Integrante_id
                 }).ToList();

            foreach (var p in consulta)
            {
                actividad = new Actividad();
                actividad.id = p.id;
                actividad.nombre = p.nombre;
                actividad.fecha_inicio = p.fecha_inicio;
                actividad.fecha_fin = p.fecha_fin;
                actividad.descripcion = p.descripcion;
                actividad.Proyecto_id = p.Proyecto_id;
                actividad.Integrante_id = p.Integrante_id;
                actividades.Add(actividad);
            }

            return actividades;
        }

        /**
       * Metodo para traer los integrantes de un proyecto
       * */
        public List<Integrante> listaIntegrantesProyectos(int idPro)
        {

            var consulta = d.Integrante.Where(p => p.Proyecto_id == idPro).
                 Select(p => new
                 {
                     p.id,
                     p.Proyecto_id,
                     p.Cargo_id,
                     p.Usuario_id
                 }).ToList();

            foreach (var p in consulta)
            {
                integrante = new Integrante();
                integrante.id = p.id;
                integrante.Proyecto_id = p.Proyecto_id;
                integrante.Cargo_id = p.Cargo_id;
                integrante.Usuario_id = p.Usuario_id;
                integrantes.Add(integrante);
            }

            return integrantes;
        }

        /**
         * Metodo para traer los datos del usuario conectado
         * */
        public List<Usuario> listarUsu(String usu)
        {
            var consulta = d.Usuario.Where(p => p.usuario1 == usu).
                 Select(p => new
                 {
                     p.id,
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
                u.id = p.id;
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


        /**
         * Metodo para conocer que tipo de usuario es el usuario
         * y asi poder registrarlo como integrante de un proyecto
         * */
        public bool verificarIntegrante(int idUsu)
        {
           int valor=0;

           var consulta = d.Usuario.Where(u => u.id == idUsu).
                Select(u => new { u.Tipo_Usuario }).ToList();

           foreach(var a in consulta){
               valor = a.Tipo_Usuario;
           }

           if(valor==2){
               return true;
           }
           else
           {
               return false;
           }            
        }

        /**
         * Metodo para conocer que tipo de usuario es el usuario
         * y asi poder registrarlo como integrante en una actividad
         * */
        public bool integranteActividad(int idInte)
        {
            int valor = 0;
            int valor2 = 0;

            var consulta = d.Integrante.Where(u => u.id == idInte).
                 Select(u => new { u.Usuario_id }).ToList();

            foreach (var a in consulta)
            {
                valor = a.Usuario_id;
            }

            var consulta2 = d.Usuario.Where(u => u.id == valor).
                 Select(u => new { u.Tipo_Usuario }).ToList();

            foreach (var b in consulta2)
            {
                valor2 = b.Tipo_Usuario;
            }

            if (valor2 == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /**
         * Metodo para permitir la creacion de recurso tarea 
         * */

        public bool verificarRecurso(int cantidad, int idRecurso)
        {

            int dato = 0;
            int valor = 0;
            var consulta = d.Recurso.Where(r => r.id == idRecurso).
               Select(r => new { r.cantidad }).ToList();

            foreach (var a in consulta)
            {
                dato = a.cantidad;
            }
            
            valor = dato - cantidad;

            if(valor >= 0){
                return true;
            }
            else
            {
                return false;
            }
        }

        /**
         * Metodo para modificar la cantidad 
         * */
        public bool modificarCantRecurso(int cantidad, int idRecuTarea, int idRecurso)
        {
            int cantTareaRecurso = 0;
            int cantRecurso = 0;

            var consulta = d.Recurso_Tarea.Where(r => r.id == idRecuTarea).
               Select(r => new { r.cantidad }).ToList();

            foreach (var a in consulta)
            {
                cantTareaRecurso = a.cantidad;
            }

            var consulta2 = d.Recurso.Where(r => r.id == idRecurso).
               Select(r => new { r.cantidad }).ToList();

            foreach (var b in consulta2)
            {
                cantRecurso = b.cantidad;
            }

            int cantidadRequerida = cantidad - cantTareaRecurso;


            if (cantidadRequerida <= cantRecurso)
            {
                if (cantidadRequerida >= 0)
                {
                    db.descontarRecurso(cantidadRequerida, idRecurso);
                    return true;
                   
                }
                else
                {
                    int nuevoValor = cantidadRequerida * -1;
                    db.aumentarRecurso(nuevoValor, idRecurso);
                    return true;
                    
                }
            }
            else
            {
                return false;
            }          
        }


        /**
         * Metodo para aumentar la cantidad del recurso 
         * mediante la eliminacion
         * */
        public void aumentarRecusrsoEliminando(int idRecursoTarea)
        {
            int cantRecurso = 0;
            int idRecurso = 0;

            var consulta = d.Recurso_Tarea.Where(r => r.id == idRecursoTarea).
               Select(r => new { r.cantidad , r.Recurso_id}).ToList();

            foreach (var b in consulta)
            {
                cantRecurso = b.cantidad;
                idRecurso = b.Recurso_id;
            }

            db.aumentarRecurso(cantRecurso , idRecurso);
            
        }

        /**
         * Metodo para evitar o permitir la eliminacion de una tarea
         * */
        public bool verificarTarea(int idTarea)
        {

            var consulta = d.Recurso_Tarea.Where(r => r.Tarea_id == idTarea).
               Select(r => new { r.id }).ToList();
            int i = consulta.Count;

            if(i!=0){
                return false;
            }else{
                return true;
            }

        }

        /**
         * Metodo para evitar o permitir la eliminacion de una recurso
         * */
        public bool verificarRecurso(int idRecurso)
        {

            var consulta = d.Recurso_Tarea.Where(r => r.Recurso_id == idRecurso).
               Select(r => new { r.id }).ToList();
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
         * Metodo para evitar o permitir la eliminacion de un proyecto
         * */
        public bool verificarProyecto(int idProyecto)
        {
            var consulta = d.Actividad.Where(a => a.Proyecto_id == idProyecto).
               Select(a => new { a.id }).ToList();
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
         * Metodo para saber si un director ya esta en un proyecto
         * */
        public bool verificarPosibleProyecto(int idUsuario)
        {
            var consulta = d.Proyecto.Where(a => a.Usuario_id == idUsuario).
               Select(a => new { a.id }).ToList();
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
         * Metodo para evitar o permitir la eliminacion de una actividad
         * */
        public bool verificarActividad(int idActividad)
        {

            var consulta = d.Recurso_Tarea.Where(r => r.Actividad_id == idActividad).
               Select(a => new { a.id }).ToList();
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
         * Metodo para evitar o permitir la eliminacion de un proyecto
         * */
        public bool verificarProyectoCargo(int idProyecto)
        {
            var consulta = d.Cargo.Where(a => a.Proyecto_id == idProyecto).
               Select(a => new { a.id }).ToList();
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
         * Metodo para evitar o permitir la eliminacion de un proyecto
         * */
        public bool verificarProyectoIntegra(int idProyecto)
        {
            var consulta = d.Integrante.Where(a => a.Proyecto_id == idProyecto).
               Select(a => new { a.id }).ToList();
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
         * Metodo para evitar o permitir la eliminacion de un proyecto
         * */
        public bool verificarProyectoReunion(int idProyecto)
        {
            var consulta = d.Reunion.Where(a => a.Proyecto_id == idProyecto).
               Select(a => new { a.id }).ToList();
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
         * Metodo para evitar o permitir la eliminacion de un integrante
         * */
        public bool verificarIntegreanteActividad(int idInte)
        {
            var consulta = d.Actividad.Where(a => a.Integrante_id == idInte).
               Select(a => new { a.id }).ToList();
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
         * Metodo para evitar o permitir la eliminacion de un cargo
         * */
        public bool verificarCargoIntegrante(int idCargo)
        {
            var consulta = d.Integrante.Where(a => a.Cargo_id == idCargo).
               Select(a => new { a.id }).ToList();
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

        

    }
    
}
