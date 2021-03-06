﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.DomainModel
{
    public class CapacityReservedEvent : IMessage
    {
        private readonly Guid id;
        private readonly int quantity;

        public CapacityReservedEvent(Guid id, int quantity)
        {
            this.id = id;
            this.quantity = quantity;
        }

        protected CapacityReservedEvent(dynamic source)
        {
            this.id = source.Id;
            this.quantity = source.Quantity;
        }

        public Envelope Envelop()
        {
            return new Envelope(this, "1");
        }

        public Guid Id
        {
            get { return this.id; }
        }

        public int Quantity
        {
            get { return this.quantity; }
        }

        public class Quickening : IQuickening
        {
            public IEnumerable<IMessage> Quicken(dynamic envelope)
            {
                if (envelope.BodyType != Envelope.CreateDefaultBodyTypeFor(typeof(CapacityReservedEvent)))
                    yield break;

                yield return new CapacityReservedEvent(envelope.Body);
            }
        }
    }
}
