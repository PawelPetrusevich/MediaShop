using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaShop.Common.Models.Notification;

namespace MediaShop.DataAccess.Configurations
{
    public class NotificationConfiguration : EntityTypeConfiguration<Notification>
    {
        public NotificationConfiguration()
        {
            this.HasKey(n => n.Id);

            this.Property(n => n.Message)
                .IsRequired()
                .IsUnicode()
                .IsVariableLength();
            this.Property(n => n.Title)
                .IsRequired()
                .IsUnicode();
            this.Property(n => n.ModifiedDate)
                .IsRequired();
            this.Property(n => n.ModifierId)
                .IsRequired();
            this.Property(n => n.ReceiverId)
                .IsRequired();

            this.HasRequired(n => n.Receiver)               //requried userId
                .WithMany()                                 //1 user can use many devices
                .HasForeignKey(user => user.ReceiverId);    //foreignkey
        }
    }
}
