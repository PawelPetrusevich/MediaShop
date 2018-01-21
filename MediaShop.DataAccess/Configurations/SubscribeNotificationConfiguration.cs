using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaShop.Common.Models.Notification;

namespace MediaShop.DataAccess.Configurations
{
    public class SubscribeNotificationConfiguration : EntityTypeConfiguration<NotificationSubscribedUser>
    {
        public SubscribeNotificationConfiguration()
        {
            this.HasKey(n => n.Id);

            this.Property(n => n.UserId)
                .IsRequired();
            this.Property(n => n.DeviceIdentifier)
                .IsRequired();
            this.Property(n => n.ModifiedDate)
                .IsRequired();
            this.Property(n => n.ModifierId)
                .IsRequired();

            this.HasRequired(n => n.Account)            //requried userId
                .WithMany()                             //1 user can use many devices
                .HasForeignKey(user => user.UserId);    //foreignkey
        }
    }
}
