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

    public virtual DbSet<CitasMedica> CitasMedicas { get; set; }

    public virtual DbSet<Doctore> Doctores { get; set; }

    public virtual DbSet<Especialidade> Especialidades { get; set; }

    public virtual DbSet<HistoriaClinica> HistoriaClinicas { get; set; }

    public virtual DbSet<InformacionesMedica> InformacionesMedicas { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<Medicamento> Medicamentos { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    public virtual DbSet<RegistroDeEntrega> RegistroDeEntregas { get; set; }

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

        modelBuilder.Entity<CitasMedica>(entity =>
        {
            entity.HasKey(e => e.IdcitasMedicas).HasName("PRIMARY");

            entity.ToTable("citas_medicas");

            entity.HasIndex(e => e.Doctor, "doctor_fk_idx");

            entity.HasIndex(e => e.TipoConsulta, "tipos_consultas_idx");

            entity.HasIndex(e => e.Paciente, "usuarios_fk_idx");

            entity.Property(e => e.IdcitasMedicas).HasColumnName("idcitas_medicas");
            entity.Property(e => e.Doctor).HasColumnName("doctor");
            entity.Property(e => e.Fecha)
                .HasMaxLength(18)
                .HasColumnName("fecha");
            entity.Property(e => e.Paciente).HasColumnName("paciente");
            entity.Property(e => e.TipoConsulta).HasColumnName("tipoConsulta");
            entity.Property(e => e.Visible).HasColumnName("visible");

            entity.HasOne(d => d.DoctorNavigation).WithMany(p => p.CitasMedicas)
                .HasForeignKey(d => d.Doctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("doctor_fk");

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

        modelBuilder.Entity<Especialidade>(entity =>
        {
            entity.HasKey(e => e.IdEspecialidades).HasName("PRIMARY");

            entity.ToTable("especialidades");

            entity.Property(e => e.IdEspecialidades).HasColumnName("idEspecialidades");
            entity.Property(e => e.Especialidad)
                .HasMaxLength(45)
                .HasColumnName("especialidad");
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

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.IdInventario).HasName("PRIMARY");

            entity.ToTable("inventario");

            entity.HasIndex(e => e.Medicamentofk, "fk_medicamento_idx");

            entity.Property(e => e.IdInventario).HasColumnName("idInventario");
            entity.Property(e => e.CantidadDisponible).HasColumnName("cantidad_disponible");
            entity.Property(e => e.Medicamentofk).HasColumnName("medicamentofk");

            entity.HasOne(d => d.MedicamentofkNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.Medicamentofk)
                .HasConstraintName("fk_medicamento");
        });

        modelBuilder.Entity<Medicamento>(entity =>
        {
            entity.HasKey(e => e.Idmedicamento).HasName("PRIMARY");

            entity.ToTable("medicamento");

            entity.Property(e => e.Idmedicamento).HasColumnName("idmedicamento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
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

        modelBuilder.Entity<RegistroDeEntrega>(entity =>
        {
            entity.HasKey(e => e.IdRegistroDeEntrega).HasName("PRIMARY");

            entity.ToTable("registro_de_entrega");

            entity.HasIndex(e => e.FarmaceuticoFkk, "farmaceuticofkk_idx");

            entity.HasIndex(e => e.MedicamentoFkk, "medicamento_fk_idx");

            entity.HasIndex(e => e.PacienteFkk, "paciente_fkk_idx");

            entity.Property(e => e.IdRegistroDeEntrega).HasColumnName("idRegistro_de_entrega");
            entity.Property(e => e.CantidadDeEntrega).HasColumnName("cantidad_de_entrega");
            entity.Property(e => e.FarmaceuticoFkk).HasColumnName("farmaceutico_fkk");
            entity.Property(e => e.FechaDeEntrega)
                .HasMaxLength(45)
                .HasColumnName("fecha_de_entrega");
            entity.Property(e => e.MedicamentoFkk).HasColumnName("medicamento_fkk");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(45)
                .HasColumnName("observaciones");
            entity.Property(e => e.PacienteFkk).HasColumnName("paciente_fkk");

            entity.HasOne(d => d.FarmaceuticoFkkNavigation).WithMany(p => p.RegistroDeEntregas)
                .HasForeignKey(d => d.FarmaceuticoFkk)
                .HasConstraintName("farmaceuticofkk");

            entity.HasOne(d => d.MedicamentoFkkNavigation).WithMany(p => p.RegistroDeEntregas)
                .HasForeignKey(d => d.MedicamentoFkk)
                .HasConstraintName("medicamento_fk");

            entity.HasOne(d => d.PacienteFkkNavigation).WithMany(p => p.RegistroDeEntregas)
                .HasForeignKey(d => d.PacienteFkk)
                .HasConstraintName("paciente_fkk");
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
