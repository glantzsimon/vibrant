using K9.Base.DataAccessLayer.Attributes;
using K9.Base.DataAccessLayer.Models;
using K9.DataAccessLayer.Enums;
using K9.SharedLibrary.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using K9.SharedLibrary.Extensions;

namespace K9.DataAccessLayer.Models
{
    [Name(ResourceType = typeof(Globalisation.Dictionary), ListName = Globalisation.Strings.Names.Orders, PluralName = Globalisation.Strings.Names.Orders, Name = Globalisation.Strings.Names.Order)]
    public class Order : ObjectBase
    {
        public Guid ExternalId { get; set; }

        [UIHint("User")]
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        [LinkedColumn(LinkedTableName = "User", LinkedColumnName = "Username")]
        public string UserName { get; set; }

        [UIHint("OrderType")]
        [Required]
        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.OrderTypeLabel)]
        public EOrderType OrderType { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.RequestedOnLabel)]
        [Required]
        public DateTime RequestedOn { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.StartedOnLabel)]
        public DateTime? StartedOn { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.DueByLabel)]
        public DateTime? DueBy { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.IsCompleteLabel)]
        public bool IsOverdue => DueBy != null && DateTime.Today > DueBy;

        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.MadeOnLabel)]
        public DateTime? MadeOn { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.IsCompleteLabel)]
        public bool IsMade => MadeOn != null && MadeOn >= DateTime.Today;

        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.StartedOnLabel)]
        public DateTime? CompletedOn { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.IsCompleteLabel)]
        public bool IsComplete => CompletedOn != null && CompletedOn <= DateTime.Today;

        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.StartedOnLabel)]
        public DateTime? PaidOn { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.IsPaidLabel)]
        public bool IsPaid => PaidOn != null && PaidOn <= DateTime.Today;

        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.OrderStatusLabel)]
        public EOrderStatus OrderStatus => GetOrderStatus();

        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.OrderStatusLabel)]
        public string OrderStatusText => OrderStatus.GetAttribute<EnumDescriptionAttribute>().GetDescription();

        [UIHint("Contact")]
        [ForeignKey("Contact")]
        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.CustomerLabel)]
        public int? ContactId { get; set; }

        public virtual Contact Contact { get; set; }

        [LinkedColumn(LinkedTableName = "Contact", LinkedColumnName = "FullName")]
        public string ContactName { get; set; }

        [UIHint("OrderOrderItems")]
        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.OrderItemsLabel)]
        public int OrderItemsId => Id;


        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PriceLabel)]
        [DataType(DataType.Currency)]
        public double Price => OrderItems?.Sum(e => e.Price) ?? 0;

        public virtual IEnumerable<OrderItem> OrderItems { get; set; }

        [NotMapped]
        public List<OrderItemProduct> Products { get; set; }

        [NotMapped]
        public List<OrderItemProductPack> ProductPacks { get; set; }

        private EOrderStatus GetOrderStatus()
        {
            if (!StartedOn.HasValue)
            {
                return EOrderStatus.InPreparation;
            }

            if (!MadeOn.HasValue)
            {
                return EOrderStatus.InProgress;
            }

            if (!CompletedOn.HasValue)
            {
                return EOrderStatus.ReadyForDelivery;
            }

            return EOrderStatus.Complete;
        }
    }
}
