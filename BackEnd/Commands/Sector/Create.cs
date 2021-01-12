﻿using Backend.extensions;
using Domain;
using LyokoAPI.API;
using LyokoAPI.Events;

namespace Backend.Commands.SectorCommand
{
    public class Create : Command
    {
        public override string Name => "create";
        public override string Usage => "sector.create.[world].[sector].[tower]";
        public override int MinArgs => 2;

        protected override void DoCommand(string[] args)
        {
            int towers = 10;
            if (args.Length > 2)
            {
                towers = CheckNumber(2);
            }
            var virtualworld = Network.GetOrCreate().GetVirtualWorld(args[0]);
            if (virtualworld == null)
            {
                throw new CommandException(this, "That virtual world does not exist!");
            }
            if (virtualworld.GetSector(args[1]) == null)
            {
                SectorCreationEvent.Call(virtualworld.Name, args[1], towers);
                virtualworld.AddSector(args[1], towers);
                Output($"Created Sector {args[1]} in {virtualworld.Name} with {towers} towers");
            }
            else
            {
                Output($"Sector {args[1]} already exists!");
            }
        }
    }
}