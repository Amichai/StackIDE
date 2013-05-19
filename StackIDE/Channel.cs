using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StackIDE {
    class Channel {
        public Channel() {
            this.channels = new Dictionary<string, List<Action>>();
        }
        public void Register(string channelName, Action action){
            if (!this.channels.ContainsKey(channelName)) {
                this.channels[channelName] = new List<Action>();
            }
            this.channels[channelName].Add(action);
        }

        Dictionary<string, List<Action>> channels { get; set; }

        public void Broadcast(string channelName) {
            foreach (var action in channels[channelName]) {
                
                action();
            }
        }
    }
}
