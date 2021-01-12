using System.Linq;
using LyokoAPI.Events.LWEvents;
using LyokoAPI.RealWorld.Location;
using LyokoAPI.VirtualEntities.LyokoWarrior;

namespace Backend.Commands.LyokoWarrior
{
    public class Translate : Command
    {
        public override string Name => "translate";
        public override string Usage => "lw.translate.[warrior]";
        public override int MinArgs => 1;
        protected override void DoCommand(string[] args)
        {
            LyokoAPI.VirtualEntities.LyokoWarrior.LyokoWarrior warrior = LyokoWarriors.GetByName(args[0].ToLower());
            if (warrior == null)
            {
                throw new CommandException(this,"Invalid warrior!");
                return;
            }
            LW_TranslationEvent.Call(warrior,new APILocation("Brazil"));
            Output(warrior.WarriorName + " translated.");
        }
    }
}