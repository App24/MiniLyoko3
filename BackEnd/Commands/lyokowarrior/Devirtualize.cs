using LyokoAPI.Events.LWEvents;
using LyokoAPI.Events.OVEvents;
using LyokoAPI.VirtualEntities.LyokoWarrior;
using LyokoAPI.VirtualEntities.Overvehicle;

namespace Backend.Commands.LyokoWarrior
{
    public class Devirtualize : Command
    {
        public override string Name => "devirt";
        public override string Usage => "lw.devirt.[warrior]";
        public override int MinArgs => 1;

        protected override void DoCommand(string[] args)
        {
            LyokoAPI.VirtualEntities.LyokoWarrior.LyokoWarrior warrior = LyokoWarriors.GetByName(args[0].ToLower());
            if (warrior == null)
            {
                throw new CommandException(this,"Invalid warrior!");
            } 
            if (warrior.CantDevirt)
            {
                throw new CommandException(this,"Can't devirt warrior!");
            }
            LyokoAPI.VirtualEntities.Overvehicle.Overvehicle overvehicle = Overvehicles.GetByWarrior(warrior);
            if (overvehicle != null)
            {
                OV_GetOffEvent.Call(overvehicle, warrior);
            }
            LW_DevirtEvent.Call(warrior);
            Output(warrior.WarriorName + " devirtualized.");
        }
    }
}