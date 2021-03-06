﻿// Copyright (c) Petabridge <https://petabridge.com/>. All rights reserved.
// Licensed under the Apache 2.0 license. See LICENSE file in the project root for full license information.
// See ThirdPartyNotices.txt for references to third party code used inside Helios.

using System;
using System.Diagnostics.Contracts;

namespace Helios.Channels
{
    public sealed class ActionChannelInitializer<T> : ChannelInitializer<T>
        where T : IChannel
    {
        private readonly Action<T> initializationAction;

        public ActionChannelInitializer(Action<T> initializationAction)
        {
            Contract.Requires(initializationAction != null);

            this.initializationAction = initializationAction;
        }

        protected override void InitChannel(T channel)
        {
            initializationAction(channel);
        }
    }
}

