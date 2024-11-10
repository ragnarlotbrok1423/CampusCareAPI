using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace clinic_api.Models;

public partial class CampusCareContext : DbContext
{
    public CampusCareContext()
    {
    }

    public CampusCareContext(DbContextOptions<CampusCareContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Certificado> Certificados { get; set; }

    public virtual DbSet<CitasMedica> CitasMedicas { get; set; }

    public virtual DbSet<Doctore> Doctores { get; set; }

    public virtual DbSet<DonantesSangre> DonantesSangres { get; set; }

    public virtual DbSet<Especialidade> Especialidades { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<HistoriaClinica> HistoriaClinicas { get; set; }

    public virtual DbSet<InformacionesMedica> InformacionesMedicas { get; set; }

    public virtual DbSet<Medicamento> Medicamentos { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Receta> Recetas { get; set; }

    public virtual DbSet<Referencia> Referencias { get; set; }

    public virtual DbSet<TipajesSanguineo> TipajesSanguineos { get; set; }

    public virtual DbSet<TiposConsulta> TiposConsultas { get; set; }

    public virtual DbSet<Usuariosxhistorialmedico> Usuariosxhistorialmedicos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=campus_care;uid=root;pwd=1234", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.34-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PRIMARY");

            entity.ToTable("categorias");

            entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");
            entity.Property(e => e.NombreCategoria)
                .HasMaxLength(45)
                .HasColumnName("nombreCategoria");
        });

        modelBuilder.Entity<Certificado>(entity =>
        {
            entity.HasKey(e => e.Idcertificados).HasName("PRIMARY");

            entity.ToTable("certificados");

            entity.HasIndex(e => e.DoctorFk, "doctor_certificado_idx");

            entity.HasIndex(e => e.PacienteFk, "paciente_certificado_idx");

            entity.Property(e => e.Idcertificados).HasColumnName("idcertificados");
            entity.Property(e => e.DoctorFk).HasColumnName("doctor_fk");
            entity.Property(e => e.Fecha)
                .HasMaxLength(19)
                .HasColumnName("fecha");
            entity.Property(e => e.Motivo)
                .HasMaxLength(45)
                .HasColumnName("motivo");
            entity.Property(e => e.PacienteFk).HasColumnName("paciente_fk");
            entity.Property(e => e.Pdf).HasColumnName("pdf");

            entity.HasOne(d => d.DoctorFkNavigation).WithMany(p => p.Certificados)
                .HasForeignKey(d => d.DoctorFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("doctor_certificado");

            entity.HasOne(d => d.PacienteFkNavigation).WithMany(p => p.Certificados)
                .HasForeignKey(d => d.PacienteFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("paciente_certificado");
        });

        modelBuilder.Entity<CitasMedica>(entity =>
        {
            entity.HasKey(e => e.IdcitasMedicas).HasName("PRIMARY");

            entity.ToTable("citas_medicas");

            entity.HasIndex(e => e.Doctor, "doctor_fk_idx");

            entity.HasIndex(e => e.Estado, "estado_fk_idx");

            entity.HasIndex(e => e.TipoConsulta, "tipos_consultas_idx");

            entity.HasIndex(e => e.Paciente, "usuarios_fk_idx");

            entity.Property(e => e.IdcitasMedicas).HasColumnName("idcitas_medicas");
            entity.Property(e => e.Doctor).HasColumnName("doctor");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasMaxLength(19)
                .HasColumnName("fecha");
            entity.Property(e => e.Paciente).HasColumnName("paciente");
            entity.Property(e => e.TipoConsulta).HasColumnName("tipoConsulta");
            entity.Property(e => e.Visible).HasColumnName("visible");

            entity.HasOne(d => d.DoctorNavigation).WithMany(p => p.CitasMedicas)
                .HasForeignKey(d => d.Doctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("doctor_fk");

            entity.HasOne(d => d.EstadoNavigation).WithMany(p => p.CitasMedicas)
                .HasForeignKey(d => d.Estado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("estado_fk");

            entity.HasOne(d => d.PacienteNavigation).WithMany(p => p.CitasMedicas)
                .HasForeignKey(d => d.Paciente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usuarios_fk");

            entity.HasOne(d => d.TipoConsultaNavigation).WithMany(p => p.CitasMedicas)
                .HasForeignKey(d => d.TipoConsulta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tipos_consultas_fk");
        });

        modelBuilder.Entity<Doctore>(entity =>
        {
            entity.HasKey(e => e.IdDoctores).HasName("PRIMARY");

            entity.ToTable("doctores");

            entity.HasIndex(e => e.EspecialidadFk, "especialidades_fk_doctores_idx");

            entity.HasIndex(e => e.InformacionMedica, "infoMedica_fk_idx");

            entity.Property(e => e.IdDoctores).HasColumnName("idDoctores");
            entity.Property(e => e.Cedula)
                .HasMaxLength(45)
                .HasColumnName("cedula");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(45)
                .HasColumnName("contraseña");
            entity.Property(e => e.Diploma).HasColumnName("diploma");
            entity.Property(e => e.EspecialidadFk).HasColumnName("especialidad_fk");
            entity.Property(e => e.InformacionMedica).HasColumnName("informacion_medica");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(45)
                .HasColumnName("nombreCompleto");
            entity.Property(e => e.Perfil).HasColumnName("perfil");

            entity.HasOne(d => d.EspecialidadFkNavigation).WithMany(p => p.Doctores)
                .HasForeignKey(d => d.EspecialidadFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("especialidades_fk_doctores");

            entity.HasOne(d => d.InformacionMedicaNavigation).WithMany(p => p.Doctores)
                .HasForeignKey(d => d.InformacionMedica)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("infoMedica_fk_doctores");
        });

        modelBuilder.Entity<DonantesSangre>(entity =>
        {
            entity.HasKey(e => e.IddonantesSangre).HasName("PRIMARY");

            entity.ToTable("donantes_sangre");

            entity.HasIndex(e => e.PacienteFk, "donante_fk_idx");

            entity.HasIndex(e => e.DoctorFk, "enfermera_fk_idx");

            entity.Property(e => e.IddonantesSangre).HasColumnName("iddonantes_sangre");
            entity.Property(e => e.DoctorFk).HasColumnName("doctor_fk");
            entity.Property(e => e.Fecha)
                .HasMaxLength(19)
                .HasColumnName("fecha");
            entity.Property(e => e.PacienteFk).HasColumnName("paciente_fk");

            entity.HasOne(d => d.DoctorFkNavigation).WithMany(p => p.DonantesSangres)
                .HasForeignKey(d => d.DoctorFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("doctor_dn_fk");

            entity.HasOne(d => d.PacienteFkNavigation).WithMany(p => p.DonantesSangres)
                .HasForeignKey(d => d.PacienteFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("donante");
        });

        modelBuilder.Entity<Especialidade>(entity =>
        {
            entity.HasKey(e => e.IdEspecialidades).HasName("PRIMARY");

            entity.ToTable("especialidades");

            entity.Property(e => e.IdEspecialidades).HasColumnName("idEspecialidades");
            entity.Property(e => e.Especialidad)
                .HasMaxLength(45)
                .HasColumnName("especialidad");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.Idestados).HasName("PRIMARY");

            entity.ToTable("estados");

            entity.Property(e => e.Idestados).HasColumnName("idestados");
            entity.Property(e => e.NombreEstado)
                .HasMaxLength(30)
                .HasColumnName("nombre_estado");
        });

        modelBuilder.Entity<HistoriaClinica>(entity =>
        {
            entity.HasKey(e => e.IdhistoriaClinica).HasName("PRIMARY");

            entity.ToTable("historia_clinica");

            entity.Property(e => e.IdhistoriaClinica).HasColumnName("idhistoria_clinica");
            entity.Property(e => e.Glisemia).HasColumnName("glisemia");
            entity.Property(e => e.Peso).HasColumnName("peso");
            entity.Property(e => e.Presion).HasColumnName("presion");
            entity.Property(e => e.Temperatura).HasColumnName("temperatura");
        });

        modelBuilder.Entity<InformacionesMedica>(entity =>
        {
            entity.HasKey(e => e.IdinformacionesMedicas).HasName("PRIMARY");

            entity.ToTable("informaciones_medicas");

            entity.HasIndex(e => e.Tipaje, "tipaje_fk_idx");

            entity.Property(e => e.IdinformacionesMedicas).HasColumnName("idinformaciones_medicas");
            entity.Property(e => e.Alergia)
                .HasColumnType("text")
                .HasColumnName("alergia");
            entity.Property(e => e.NumeroSecundario)
                .HasMaxLength(45)
                .HasColumnName("numero_secundario");
            entity.Property(e => e.Tipaje).HasColumnName("tipaje");

            entity.HasOne(d => d.TipajeNavigation).WithMany(p => p.InformacionesMedicas)
                .HasForeignKey(d => d.Tipaje)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tipaje_fk");
        });

        modelBuilder.Entity<Medicamento>(entity =>
        {
            entity.HasKey(e => e.Idmedicamento).HasName("PRIMARY");

            entity.ToTable("medicamentos");

            entity.HasIndex(e => e.CategoriaFk, "fk_categoria_idx");

            entity.Property(e => e.Idmedicamento).HasColumnName("idmedicamento");
            entity.Property(e => e.CantidadStock).HasColumnName("cantidadStock");
            entity.Property(e => e.CategoriaFk).HasColumnName("categoria_fk");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");

            entity.HasOne(d => d.CategoriaFkNavigation).WithMany(p => p.Medicamentos)
                .HasForeignKey(d => d.CategoriaFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_categoria");
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.IdPacientes).HasName("PRIMARY");

            entity.ToTable("pacientes");

            entity.HasIndex(e => e.InformacionMedica, "informacion_medica_fk_idx");

            entity.Property(e => e.IdPacientes).HasColumnName("idPacientes");
            entity.Property(e => e.Apellido)
                .HasMaxLength(30)
                .HasColumnName("apellido");
            entity.Property(e => e.Cedula)
                .HasMaxLength(10)
                .HasColumnName("cedula");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(45)
                .HasColumnName("contraseña");
            entity.Property(e => e.InformacionMedica).HasColumnName("informacion_medica");
            entity.Property(e => e.Nombre)
                .HasMaxLength(10)
                .HasColumnName("nombre");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(45)
                .HasColumnName("nombreUsuario");
            entity.Property(e => e.Visibiliy).HasColumnName("visibiliy");

            entity.HasOne(d => d.InformacionMedicaNavigation).WithMany(p => p.Pacientes)
                .HasForeignKey(d => d.InformacionMedica)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("informacion_medica_fk_pacientes");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.Idpedidos).HasName("PRIMARY");

            entity.ToTable("pedidos");

            entity.HasIndex(e => e.FarmaceuticoFk, "fk_farmaceutico_idx");

            entity.HasIndex(e => e.MedicamentoFk, "medicamento_pedido_fk_idx");

            entity.Property(e => e.Idpedidos).HasColumnName("idpedidos");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.FarmaceuticoFk).HasColumnName("farmaceutico_fk");
            entity.Property(e => e.Fecha)
                .HasMaxLength(19)
                .HasColumnName("fecha");
            entity.Property(e => e.MedicamentoFk).HasColumnName("medicamento_fk");

            entity.HasOne(d => d.FarmaceuticoFkNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.FarmaceuticoFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("farmaceutico_pedido");

            entity.HasOne(d => d.MedicamentoFkNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.MedicamentoFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("medicamento_pedido_fk");
        });

        modelBuilder.Entity<Receta>(entity =>
        {
            entity.HasKey(e => e.IdRegistroDeEntrega).HasName("PRIMARY");

            entity.ToTable("recetas");

            entity.HasIndex(e => e.FarmaceuticoFkk, "farmaceuticofkk_idx");

            entity.HasIndex(e => e.MedicamentoFk, "fk_medicamentos_idx");

            entity.HasIndex(e => e.PacienteFkk, "paciente_fkk_idx");

            entity.Property(e => e.IdRegistroDeEntrega).HasColumnName("idRegistro_de_entrega");
            entity.Property(e => e.CantidadDeEntrega).HasColumnName("cantidad_de_entrega");
            entity.Property(e => e.FarmaceuticoFkk).HasColumnName("farmaceutico_fkk");
            entity.Property(e => e.FechaDeEntrega)
                .HasMaxLength(45)
                .HasColumnName("fecha_de_entrega");
            entity.Property(e => e.MedicamentoFk).HasColumnName("medicamento_fk");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(45)
                .HasColumnName("observaciones");
            entity.Property(e => e.PacienteFkk).HasColumnName("paciente_fkk");

            entity.HasOne(d => d.FarmaceuticoFkkNavigation).WithMany(p => p.Receta)
                .HasForeignKey(d => d.FarmaceuticoFkk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("farmaceuticofkk");

            entity.HasOne(d => d.MedicamentoFkNavigation).WithMany(p => p.Receta)
                .HasForeignKey(d => d.MedicamentoFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_medicamentos");

            entity.HasOne(d => d.PacienteFkkNavigation).WithMany(p => p.Receta)
                .HasForeignKey(d => d.PacienteFkk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("paciente_fkk");
        });

        modelBuilder.Entity<Referencia>(entity =>
        {
            entity.HasKey(e => e.Idreferencias).HasName("PRIMARY");

            entity.ToTable("referencias");

            entity.HasIndex(e => e.DoctorFk, "doctor_referencia_idx");

            entity.HasIndex(e => e.PacienteFk, "paciente_referencia_idx");

            entity.Property(e => e.Idreferencias).HasColumnName("idreferencias");
            entity.Property(e => e.CondicionMedica)
                .HasMaxLength(45)
                .HasColumnName("condicion_medica");
            entity.Property(e => e.Diagnostico)
                .HasMaxLength(45)
                .HasColumnName("diagnostico");
            entity.Property(e => e.DoctorFk).HasColumnName("doctor_fk");
            entity.Property(e => e.Especialidad)
                .HasMaxLength(45)
                .HasColumnName("especialidad");
            entity.Property(e => e.Fecha)
                .HasMaxLength(19)
                .HasColumnName("fecha");
            entity.Property(e => e.PacienteFk).HasColumnName("paciente_fk");
            entity.Property(e => e.Pdf).HasColumnName("pdf");
            entity.Property(e => e.Sintomas)
                .HasMaxLength(45)
                .HasColumnName("sintomas");

            entity.HasOne(d => d.DoctorFkNavigation).WithMany(p => p.Referencia)
                .HasForeignKey(d => d.DoctorFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("doctor_referencia");

            entity.HasOne(d => d.PacienteFkNavigation).WithMany(p => p.Referencia)
                .HasForeignKey(d => d.PacienteFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("paciente_referencia");
        });

        modelBuilder.Entity<TipajesSanguineo>(entity =>
        {
            entity.HasKey(e => e.IdtipajesSanguineos).HasName("PRIMARY");

            entity.ToTable("tipajes_sanguineos");

            entity.Property(e => e.IdtipajesSanguineos).HasColumnName("idtipajes_sanguineos");
            entity.Property(e => e.TipoSanguineo)
                .HasMaxLength(3)
                .HasColumnName("tipo_sanguineo");
        });

        modelBuilder.Entity<TiposConsulta>(entity =>
        {
            entity.HasKey(e => e.IdtiposConsultas).HasName("PRIMARY");

            entity.ToTable("tipos_consultas");

            entity.Property(e => e.IdtiposConsultas).HasColumnName("idtipos_consultas");
            entity.Property(e => e.TipoConsulta)
                .HasMaxLength(45)
                .HasColumnName("tipo_consulta");
        });

        modelBuilder.Entity<Usuariosxhistorialmedico>(entity =>
        {
            entity.HasKey(e => e.IdusuariosXhistorialMedico).HasName("PRIMARY");

            entity.ToTable("usuariosxhistorialmedico");

            entity.HasIndex(e => e.HistorialMedico, "historialMedicoXpacientes_idx");

            entity.HasIndex(e => e.Pacientes, "pacientesXhistorialMedico_idx");

            entity.Property(e => e.IdusuariosXhistorialMedico).HasColumnName("idusuariosXHistorialMedico");
            entity.Property(e => e.HistorialMedico).HasColumnName("historial_medico");
            entity.Property(e => e.Pacientes).HasColumnName("pacientes");

            entity.HasOne(d => d.HistorialMedicoNavigation).WithMany(p => p.Usuariosxhistorialmedicos)
                .HasForeignKey(d => d.HistorialMedico)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("historialMedicoXpacientes");

            entity.HasOne(d => d.PacientesNavigation).WithMany(p => p.Usuariosxhistorialmedicos)
                .HasForeignKey(d => d.Pacientes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pacientesXhistorialMedico");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
