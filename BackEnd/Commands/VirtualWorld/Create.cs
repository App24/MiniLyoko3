﻿using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LyokoAPI.Events;
using LyokoAPI.Commands;
using LyokoAPI.Exceptions;

namespace BackEnd.Commands.VirtualWorldCommand
{
    public class Create : Command
    {
        public override string Name => "create";
        public override string Usage => "world.create.<world>";
        public override int MinArgs => 1;

        protected override void DoCommand(string[] args)
        {
            VirtualWorld virtualWorld= Network.GetOrCreate().GetVirtualWorld(args[0]);
            if (virtualWorld != null)
            {
                throw new CommandException(this, $"World {args[0]} Already Exists!");
            }
            Network.GetOrCreate().AddVirtualWorld(args[0]);
            Output($"World {args[0]} Created!");
        }
    }
}
