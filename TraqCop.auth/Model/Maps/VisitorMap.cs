using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection;

namespace TraqCop.auth.Model.Maps
{
    public class VisitorMap : BaseEntityTypeConfiguration<VisitorModel>
    {
        public override void Configure(EntityTypeBuilder<VisitorModel> modelBuilder)
        {
            base.Configure(modelBuilder);
            modelBuilder.Property(c => c.ImageUrl).HasMaxLength(150);
            modelBuilder.Property(c => c.ImageName).HasMaxLength(250);
            modelBuilder.Property(c => c.ImageFileSize).HasMaxLength(250);
            modelBuilder.Property(c => c.ImageOriginalFileName).HasMaxLength(250);

            var datas = SetupData();
            modelBuilder.HasData(datas);
            
        }
        private VisitorModel[] SetupData()
        {
            var data = new VisitorModel[]
            {
                new VisitorModel
                {
                    Id = Guid.Parse("66346adb-20e5-425d-a330-47cf649cf44d"),
                    LastName= "Joy",
                    FirstName = "Gbenga",
                    PhoneNumber = "09045756647",
                    Email = "rerdadudre@gufum.com",
                    Address = "89, ogba lagos",
                    Gender = "Female",
                    Nationality = "Nigeria",
                    State = "Ogun",
                    PurposeOfEntry ="to work",
                    CreatedOn = new DateTime(2019, 09, 16),
                    ModifiedOn = new DateTime(2019, 09, 16),
                    ModifiedBy = "sholl45@gmail.com",
                    CreatedBy = "sholl45@gmail.com",
                },
                new VisitorModel
                {
                   Id = Guid.Parse("66346adb-20e5-425d-a330-47cf649cf44d"),
                    LastName= "Ola",
                    FirstName = "Smith",
                    PhoneNumber = "09045756647",
                    Email = "rerdadudre@gufum.com",
                    Address = "89, ogba lagos",
                    Gender = "Female",
                    Nationality = "Nigeria",
                    State = "Ogun",
                    PurposeOfEntry ="to work",
                    CreatedOn = new DateTime(2019, 09, 16),
                    ModifiedOn = new DateTime(2019, 09, 16),
                    ModifiedBy = "sholl45@gmail.com",
                    CreatedBy = "sholl45@gmail.com",
                },
                new VisitorModel
                {
                     Id = Guid.Parse("66346adb-20e5-425d-a330-47cf649cf44d"),
                    LastName= "Olu",
                    FirstName = "Gold",
                    PhoneNumber = "09045756647",
                    Email = "rerdadudre@gufum.com",
                    Address = "89, ogba lagos",
                    Gender = "Female",
                    Nationality = "Nigeria",
                    State = "Ogun",
                    PurposeOfEntry ="to work",
                    CreatedOn = new DateTime(2019, 09, 16),
                    ModifiedOn = new DateTime(2019, 09, 16),
                    ModifiedBy = "sholl45@gmail.com",
                    CreatedBy = "sholl45@gmail.com",
                },
                new VisitorModel
                {
                     Id = Guid.Parse("66346adb-20e5-425d-a330-47cf649cf44d"),
                    LastName= "Femi",
                    FirstName = "John",
                    PhoneNumber = "09045756647",
                    Email = "rerdadudre@gufum.com",
                    Address = "89, ogba lagos",
                    Gender = "Female",
                    Nationality = "Nigeria",
                    State = "Ogun",
                    PurposeOfEntry ="to work",
                    CreatedOn = new DateTime(2019, 09, 16),
                    ModifiedOn = new DateTime(2019, 09, 16),
                    ModifiedBy = "sholl45@gmail.com",
                    CreatedBy = "sholl45@gmail.com",

                },
               
            };


            return data;
        }
    }
}
