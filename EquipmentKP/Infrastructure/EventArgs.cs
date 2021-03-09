using System;
using System.Collections.Generic;
using System.Text;

namespace EquipmentKP.Infrastructure
{
    class EventArgs<T> : EventArgs
    {
        public T Arg { get; set; }
        public EventArgs(T Arg) => this.Arg = Arg;
        public static implicit operator EventArgs<T>(T arg) => new EventArgs<T>(arg);
        public static implicit operator T(EventArgs<T> arg) => arg.Arg;
    }
}
