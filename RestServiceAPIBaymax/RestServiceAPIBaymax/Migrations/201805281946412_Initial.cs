namespace RestServiceAPIBaymax.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Medicinas",
                c => new
                    {
                        MedicinaId = c.Int(nullable: false, identity: true),
                        NombreMedicina = c.String(),
                        PrincipiosActivos = c.String(),
                        CodigoNacional = c.Int(nullable: false),
                        Contenido = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UnidadContenido = c.String(),
                    })
                .PrimaryKey(t => t.MedicinaId);
            
            CreateTable(
                "dbo.MedicinasUsuarios",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false, identity: true),
                        NombresMedicinas = c.String(),
                        PrincipiosActivos = c.String(),
                        CodigosNacionales = c.Int(nullable: false),
                        ContenidosIniciales = c.Int(nullable: false),
                        ContenidosActuales = c.Int(nullable: false),
                        UnidadContenido = c.String(),
                    })
                .PrimaryKey(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false, identity: true),
                        Genero = c.String(),
                        tokensUsuariosQueNotifica = c.String(),
                        tokensUsuariosQueRecibeNotificacion = c.String(),
                    })
                .PrimaryKey(t => t.UsuarioId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Usuarios");
            DropTable("dbo.MedicinasUsuarios");
            DropTable("dbo.Medicinas");
        }
    }
}
