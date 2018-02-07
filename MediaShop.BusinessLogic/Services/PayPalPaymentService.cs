namespace MediaShop.BusinessLogic.Services
{
    using System;
    using MediaShop.BusinessLogic.Properties;
    using MediaShop.Common.Exceptions.PaymentExceptions;
    using MediaShop.Common.Interfaces.Services;
    using MediaShop.Common.Models;
    using MediaShop.Common.Models.PaymentModel;
    using MediaShop.Common.Enums.PaymentEnums;
    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Dto.Payment;
    using AutoMapper;

    /// <summary>
    /// Class PaymentService
    /// </summary>
    public class PayPalPaymentService : IPayPalPaymentService
    {
        private readonly IPayPalPaymentRepository repositoryPayment;

        /// <summary>
        /// Initializes a new instance of the <see cref="PayPalPaymentService"/> class.
        /// </summary>
        /// <param name="paymentRepository">instance repository PaymentRepository</param>
        public PayPalPaymentService(IPayPalPaymentRepository paymentRepository)
        {
            this.repositoryPayment = paymentRepository;
        }

        /// <summary>
        /// Add object payment in repository
        /// </summary>
        /// <param name="payment">object Payment after decerializer</param>
        /// <returns>object payment</returns>
        public PayPalPaymentDto AddPayment(PayPalPayment payment)
        {
            // 1. Check payment for null
            if (payment == null)
            {
                throw new InvalideDecerializableExceptions(Resources.BadDeserializer);
            }

            try
            {
                // 2. Check state payment
                if (payment.State == null)
                {
                    throw new InvalideDecerializableExceptions(Resources.BadDeserializer);
                }

                var stringFirstUpper = $"{payment.State.Substring(0, 1).ToUpper()}{payment.State.Substring(1)}";

                var enumString = (PaymentStates)Enum.Parse(typeof(PaymentStates), stringFirstUpper);

                if (enumString != PaymentStates.Approved)
                {
                    switch (enumString)
                    {
                        case PaymentStates.Created:
                            throw new OperationPaymentException(Resources.NotApprovedOperationPayment);
                        case PaymentStates.Failed:
                            throw new OperationPaymentException(Resources.FailedOperationPayment);
                    }
                }

                // 3. Mapping PaymentTransaction to PaymentDbModel
                var paymentDbModel = Mapper.Map<PayPalPaymentDbModel>(payment);

                // 4. Add object PaymentDbModel in database
                var paymentResult = this.repositoryPayment.Add(paymentDbModel);

                // throw new AddPaymentException
                if (paymentResult == null)
                {
                    throw new AddPaymentException(Resources.BadAddPayment);
                }

                // else mapping Payment => PaymentDto and return user
                var paymentDto = Mapper.Map<PayPalPaymentDto>(payment);

                return paymentDto;
            }
            catch (ArgumentNullException error)
            {
                throw new ArgumentNullException(error.Message);
            }
            catch (ArgumentException error)
            {
                throw new ArgumentException(error.Message);
            }
            catch (OverflowException error)
            {
                throw new OverflowException(error.Message);
            }
        }

        public string Payment(Cart cart)
        {
            throw new System.NotImplementedException();
        }
    }
}
