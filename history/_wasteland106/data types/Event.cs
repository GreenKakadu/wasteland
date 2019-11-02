using System;
using System.Collections;

namespace Wasteland
{
	public interface ILocalized 
	{
		string ToString(Lang lng);
	}

	public class Event : ILocalized
	{
		private static Hashtable _messages = null;
		private Person _person = null;
		private string _order = "";
		private Event.Code _code;
		private object[] _params;

		public Event(Event.Code code) 
		{
			_code = code;
			_params = new object[0];
		}

		public Event(Person person, string order, Event.Code code, object[] parameters) 
		{
			_person = person;
			_order = order;
			_code = code;
			_params = parameters;
		}

		public Person Person 
		{
			get { return _person; }
		}

		public string ToString(Lang lng) 
		{
			return ToString(lng, true);
		}

		public string ToString(Lang lng, bool show_person) 
		{
			string res = "";
			
			if (show_person && _person != null)
				res += _person.ToString(lng) + ": ";

			if (_order != "")
				res += _order + ": ";

			string[] ps = new string[_params.Length];
      for (int i = 0; i < _params.Length; i++)
				if (_params[i].GetType().GetInterface("ILocalized") != null)
					ps[i] = ((ILocalized)_params[i]).ToString(lng);
				else
					ps[i] = _params[i].ToString();
			
			res += String.Format(((EventMessage)Messages[_code]).ToString(lng), ps);

			return res;
		}

		private static Hashtable Messages
		{
			// I decided to hard-code this to avoid incoherency between data.xml
			// and different code versions. If there will be attempts to add more 
			// languages, probably data file should be considered (with a thought
			// about execution speed, because this will be called very rapidly).
			get 
			{
				if (_messages == null) 
				{
					_messages = new Hashtable();

					_messages.Add(Code.OrderForLeader, new EventMessage(
						"This order should be given to leader.",
						"���� ������ ������������ ��� ��������� �������."
						));
					_messages.Add(Code.NoSuchItem, new EventMessage(
						"No such item.",
						"��� ����� ����."
						));
					_messages.Add(Code.NoSkill, new EventMessage(
						"No skill to do that.",
						"��� ������������ ������."
						));
					_messages.Add(Code.NotSkilledEnough, new EventMessage(
						"Not skilled enough.",
						"������������� ������� ������."
						));
					_messages.Add(Code.NotInside, new EventMessage(
						"Should be inside object.",
						"����� ���������� ������ �������."
						));
					_messages.Add(Code.NotInRegion, new EventMessage(
						"{0} not in region.",
						"{0} �� � �������."
						));
					_messages.Add(Code.CantAttackSameFaction, new EventMessage(
						"Can't attack a person from same faction.",
						"������ ��������� ��������� �� ����� �������."
						));
					_messages.Add(Code.CantAttackSameTeam, new EventMessage(
						"Can't attack a person from same team.",
						"������ ��������� ��������� �� ����� �������."
						));
					_messages.Add(Code.CantBuildObject, new EventMessage(
						"Can't build this object.",
						"���� ������ ������ ���������."
						));
					_messages.Add(Code.TargetNotFriendly, new EventMessage(
						"{0} is not Friendly.",
						"{0} �� ��������� � ��� ����������."
						));
					_messages.Add(Code.OutOfDrugs, new EventMessage(
						"Out of drugs.",
						"��������� ��������."
						));
					_messages.Add(Code.NoSuchFaction, new EventMessage(
						"No such faction.",
						"��� ����� �������."
						));
					_messages.Add(Code.MustOwnObject, new EventMessage(
						"Must own object to do that.",
						"����� ���� ���������� �������."
						));
					_messages.Add(Code.ObjectCantMove, new EventMessage(
						"Object can't move.",
						"������ �� ����� ���������."
						));
					_messages.Add(Code.ObjectIncomplete, new EventMessage(
						"Object is not completed.",
						"������ �� ��������."
						));
					_messages.Add(Code.VehicleOverloaded, new EventMessage(
						"Vehicle is overloaded.",
						"��������� ����������."
						));
					_messages.Add(Code.CantDriveThere, new EventMessage(
						"Can't drive there.",
						"���� ������ �� �������."
						));
					_messages.Add(Code.NotEnoughMP, new EventMessage(
						"Insufficient movement points.",
						"������������ ����� ������������."
						));
					_messages.Add(Code.RadiationTooHighToEnter, new EventMessage(
						"Tried to enter {0}, but feeled woozy and turned back.",
						"�������� ����� � {0}, �� ��������� �������� � ������� � ������������."
						));
					_messages.Add(Code.NoFuel, new EventMessage(
						"No fuel.",
						"��� ��������."
						));
					_messages.Add(Code.ObjectOwnerNotFriendly, new EventMessage(
						"Object owner is not Friendly.",
						"�������� ������� �� ��������� � ��� ������������."
						));
					_messages.Add(Code.NotInObject, new EventMessage(
						"{0} not in same object.",
						"{0} �� � ���� �������."
						));
					_messages.Add(Code.EvictLeader, new EventMessage(
						"Must EVICT a team leader.",
						"���� �������� ��������� �������."
						));
					_messages.Add(Code.Evicted, new EventMessage(
						"Evicted {0}.",
						"�������� ��������� {0}."
						));
					_messages.Add(Code.EvictedBy, new EventMessage(
						"Evicted by owner.",
						"�������� ������� �� �������."
						));
					_messages.Add(Code.NoParameters, new EventMessage(
						"No parameters given.",
						"�� ������ ���������� �������."
						));
					_messages.Add(Code.CuredBy, new EventMessage(
						"Cured by {0}.",
						"��������� ������� �� ��������� {0}."
						));
					_messages.Add(Code.Cures, new EventMessage(
						"Cures {0}.",
						"����� ��������� {0}."
						));
					_messages.Add(Code.CuresUsing, new EventMessage(
						"Cures {0} using {1}.",
						"����� ��������� {0}; ������������: {1}."
						));
					_messages.Add(Code.Drives, new EventMessage(
						"Drives from {0} to {1}.",
						"�������� {0}, �������� � {1}."
						));
					_messages.Add(Code.DrivesSpending, new EventMessage(
						"Drives from {0} to {1} spending 1 {2}.",
						"�������� {0}, �������� � {1}. ������������ 1 {2}."
						));
					_messages.Add(Code.PersonDrives, new EventMessage(
						"{0} drives from {1} to {2}.",
						"������ ��� ����������� ��������� {0} �������� {1}, �������� � {2}."
						));
					_messages.Add(Code.Enters, new EventMessage(
						"Enters {0}.",
						"������ � {0}."
						));
					_messages.Add(Code.CantHideInTeam, new EventMessage(
						"Can't hide in team.",
						"������ ��������� � �������."
						));
					_messages.Add(Code.CantHideInObject, new EventMessage(
						"Can't hide inside object.",
						"������ ��������� � �������."
						));
					_messages.Add(Code.CantInstall, new EventMessage(
						"Can't install that.",
						"���� ������� ������ �����������."
						));
					_messages.Add(Code.Leaves, new EventMessage(
						"Leaves {0}.",
						"�������� {0}."
						));
					_messages.Add(Code.Maintains, new EventMessage(
						"Maintains {0}.",
						"����������� {0}."
						));
					_messages.Add(Code.Patrols, new EventMessage(
						"Patrols {0}.",
						"����������� {0}."
						));
					_messages.Add(Code.Produces, new EventMessage(
						"Produces {0}.",
						"�����������: {0}."
						));
					_messages.Add(Code.Quitted, new EventMessage(
						"{0} committed suicide. Bones was lost in desert, but great deeds will stay in the hearts of survived.",
						"{0} ��������� ������������. ����� �������� � ������, �� ������� ���� �������� ��������� � ������ ��������."
						));
					_messages.Add(Code.NoItemInJunk, new EventMessage(
						"No such item in junk.",
						"� ������ ����� ���."
						));
					_messages.Add(Code.NotInTeam, new EventMessage(
						"{0} not in team.",
						"{0} �� � �������."
						));
					_messages.Add(Code.KickedTeam, new EventMessage(
						"Kicked from team.",
						"�������� ������ ��������� �� �������."
						));
					_messages.Add(Code.FactionDismissed, new EventMessage(
						"Faction lost Chosen One and was dismissed.",
						"������� �������� ���������� � ���������."
						));
					_messages.Add(Code.GoesInsane, new EventMessage(
						"Goes insane and leaves faction.",
						"������ � ��� � ������� �� �������."
						));
					_messages.Add(Code.Discards, new EventMessage(
						"Discards {0}.",
						"���������: {0}."
						));
					_messages.Add(Code.Receives, new EventMessage(
						"Receives {0} from {1}.|",
						"�������� �� ��������� {1}: {0}."
						));
					_messages.Add(Code.JoinedFaction, new EventMessage(
						"{0} joined your faction.",
						"{0} �������������� � �������."
						));
					_messages.Add(Code.CantInstallInObject, new EventMessage(
						"Item can't be installed in this object.",
						"������� ������ ���������� � ���� ������."
						));
					_messages.Add(Code.AnotherOptionInstalled, new EventMessage(
						"Another optional component installed.",
						"��� ���������� ������ ������������ ���������."
						));
					_messages.Add(Code.MaxAmountInstalled, new EventMessage(
						"Max amount of this item already installed.",
						"��� ����������� ������������ ����������."
						));
					_messages.Add(Code.DifferentInstallSkill, new EventMessage(
						"All materials should require same install skill.",
						"��� ���������� ������ ������ ��������� ����������� ������ ��� ���������."
						));
					_messages.Add(Code.NoItemToInstall, new EventMessage(
						"No {0} to install.",
						"��� � �������: {0}."
						));
					_messages.Add(Code.NoSkillToInstall, new EventMessage(
						"Not enough skill to install {0}.",
						"������������ ������ ��� ���������: {0}."
						));
					_messages.Add(Code.Installs, new EventMessage(
						"Performs work on {0}, installs {1}.",
						"� ������ {0} �����������: {1}."
						));
					_messages.Add(Code.CantKickChosen, new EventMessage(
						"Can't kick Chosen One.",
						"������ ������� ����������."
						));
					_messages.Add(Code.Kicked, new EventMessage(
						"{0} kicked from faction.",
						"{0} ����������� �� �������."
						));
					_messages.Add(Code.CantMoveThere, new EventMessage(
						"Can't move there.",
						"���� �� ������."
						));
					_messages.Add(Code.PersonOverloaded, new EventMessage(
						"Person is overloaded.",
						"�������� ����������."
						));
					_messages.Add(Code.Moves, new EventMessage(
						"Moves from {0} to {1}.",
						"�������� {0}, ������ � {1}."
						));
					_messages.Add(Code.EntryForbidden, new EventMessage(
						"Entry to {0} forbidden by patrol: {1}.",
						"���� � {0} �������� ����������: {1}."
						));
					_messages.Add(Code.EntryForbiddenF, new EventMessage(
						"Entry to {0} forbidden by patrol: {1}, {2}.",
						"���� � {0} �������� ����������: {1}, {2}."
						));
					_messages.Add(Code.PasswordChanged, new EventMessage(
						"Password was changed.",
						"������ ������."
						));
					_messages.Add(Code.CantProduce, new EventMessage(
						"Can't produce that.",
						"������� ������ ����������."
						));
					_messages.Add(Code.MustBeInside, new EventMessage(
						"Must be inside {0}.",
						"����� ���� ������ �������: {0}."
						));
					_messages.Add(Code.ObjectIncompleteNamed, new EventMessage(
						"Object {0} is incomplete.",
						"������ {0} �� ��������."
						));
					_messages.Add(Code.NoResource, new EventMessage(
						"No resource in region.",
						"������ ����������� � �������."
						));
					_messages.Add(Code.NotEnoughItem, new EventMessage(
						"Not enough {0}.",
						"��������� {0}."
						));
					_messages.Add(Code.Uninstalls, new EventMessage(
						"Uninstalls {0}.",
						"�������������: {0}."
						));
					_messages.Add(Code.UninstallsCollapsed, new EventMessage(
						"Uninstalls {0}. Object collapsed.",
						"�������������: {0}. ������ ��������."
						));
					_messages.Add(Code.NoItemInstalled, new EventMessage(
						"No {0} installed.",
						"� ������� ��� ����������: {0}."
						));
					_messages.Add(Code.NoSkillToUninstall, new EventMessage(
						"Not enough skill to uninstall {0}.",
						"������ ������������, ����� �������������: {0}."
						));
					_messages.Add(Code.FactionMustOwnObject, new EventMessage(
						"A person of same faction must own object.",
						"�������� ����� ������� ������ ���� ���������� �������."
						));
					_messages.Add(Code.IsInTeam, new EventMessage(
						"{0} is in team.",
						"{0} ���� ��������� � �������."
						));
					_messages.Add(Code.CantJoinSelf, new EventMessage(
						"Leader can't be same person.",
						"�� ����� ����� ������� ����."
						));
					_messages.Add(Code.JoinedTeam, new EventMessage(
						"{0} joined team.",
						"{0} �������������� � �������."
						));
					_messages.Add(Code.LeavesTeam, new EventMessage(
						"{0} leaved team.",
						"{0} �������� �������."
						));
					_messages.Add(Code.NotEnoughOffer, new EventMessage(
						"Not enough {0} to offer.",
						"������������ ���������: {0}."
						));
					_messages.Add(Code.Trades, new EventMessage(
						"Trades {0} for {1} with {2}.",
						"��������� {2} ������: {0} ��: {1}."
						));
					_messages.Add(Code.BornBaby, new EventMessage(
						"Born the baby: {0}, {1}.",
						"������ ������: {0}, {1}."
						));
					_messages.Add(Code.HasBabyFrom, new EventMessage(
						"Has baby from {0}.",
						"������� ������ �� ��������� {0}."
						));
					_messages.Add(Code.HadSexWith, new EventMessage(
						"Had sex with {0}.",
						"���� � ���������� {0}."
						));
					_messages.Add(Code.TeamQuarrel, new EventMessage(
						"Persons in team are quarrelling.",
						"��������� � ������� ��������."
						));
					_messages.Add(Code.Scavenges, new EventMessage(
						"Scavenges {0}.",
						"������� � ������: {0}."
						));
					_messages.Add(Code.Starved, new EventMessage(
						"Starved to death.",
						"������� �� ���������."
						));
					_messages.Add(Code.Hungry, new EventMessage(
						"Hungry, needs {0} more rations.",
						"��������, ����� ��� {0} ��������."
						));
					_messages.Add(Code.Mutated, new EventMessage(
						"Mutated to {0}!",
						"�������� - ������ ��� {0}!"
						));
					_messages.Add(Code.RadiationOverdose, new EventMessage(
						"Died from lethal dose of radiation.",
						"������� �� ��������� ���� ��������."
						));
					_messages.Add(Code.RadiationUnderdose, new EventMessage(
						"Died from lack of radiation.",
						"������� �� ���������� ��������."
						));
					_messages.Add(Code.RadiationReceived, new EventMessage(
						"Receiving a dose of radiation.",
						"�������� ���� ��������."
						));
					_messages.Add(Code.RadiationNotReceived, new EventMessage(
						"Not receiving a vital dose of radiation.",
						"�� �������� ����������� ���� ��������."
						));
					_messages.Add(Code.Frozen, new EventMessage(
						"Frozen to death.",
						"������� �� ��������������."
						));
					_messages.Add(Code.Cold, new EventMessage(
						"Suffers from cold, {0}� below acceptable.",
						"�������� �� ������, {0}� ���� �����."
						));
					_messages.Add(Code.Burned, new EventMessage(
						"Burned {0}",
						"�������: {0}"
						));
					_messages.Add(Code.Borrows, new EventMessage(
						"Borrows from {0} {1}.",
						"{0} ����������: {1}."
						));
					_messages.Add(Code.Address, new EventMessage(
						"Address is now {0}.",
						"����� ������ �� {0}."
						));
					_messages.Add(Code.Rehired, new EventMessage(
						"{0} leaves faction and joins {1}.",
						"{0} �������� �������, ��������� ���������� {1}."
						));
					_messages.Add(Code.NotFriendlyHireFail, new EventMessage(
						"{0} is not friendly and does not want it. It was not enough to hire this person.",
						"{0} �� ��������� � ��� ������������ � ������ �� ����� �����. ����� ������������, ����� ���������� ������� ���������."
						));
					_messages.Add(Code.RefusesReceive, new EventMessage(
						"Refused to receive {0} from {1}.",
						"������������ �� �������� {0}, ������������� ���������� {1}."
						));
					_messages.Add(Code.GivesNoHire, new EventMessage(
						"Gives {0} to {1}. It was not enough to hire this person.",
						"������ ��������� {1}: {0}. ����� ������������, ����� ���������� ������� ���������."
						));
					_messages.Add(Code.Gives, new EventMessage(
						"Gives {0} to {1}.",
						"������ ��������� {1}: {0}."
						));
					_messages.Add(Code.Consumed, new EventMessage(
						"Consumes {0}",
						"�������: {0}"
						));
				}
				return _messages;
			}
		}

		public enum Code 
		{
			Address,
			AnotherOptionInstalled,
			BornBaby,
			Burned,
			Borrows,
			CantAttackSameFaction,
			CantAttackSameTeam,
			CantBuildObject,
			CantDriveThere,
			CantHideInTeam,
			CantHideInObject,
			CantInstall,
			CantInstallInObject,
			CantJoinSelf,
			CantKickChosen,
			CantMoveThere,
			CantProduce,
			Cold,
			Consumed,
			Cures,
			CuresUsing,
			CuredBy,
			DifferentInstallSkill,
			Discards,
			Drives,
			DrivesSpending,
			Enters,
			EntryForbidden,
			EntryForbiddenF,
			EvictLeader,
			Evicted,
			EvictedBy,
			FactionDismissed,
			FactionMustOwnObject,
			Frozen,
			GoesInsane,
			Gives,
			GivesNoHire,
			HasBabyFrom,
			HadSexWith,
			Hungry,
			Installs,
			IsInTeam,
			JoinedFaction,
			JoinedTeam,
			Kicked,
			KickedTeam,
			Leaves,
			LeavesTeam,
			Maintains,
			MaxAmountInstalled,
			Moves,
			MustBeInside,
			MustOwnObject,
			Mutated,
			NoFuel,
			NoItemToInstall,
			NoResource,
			NoSuchItem,
			NoSkill,
			NoSkillToInstall,
			NoItemInJunk,
			NoItemInstalled,
			NotSkilledEnough,
			NotInside,
			NotInRegion,
			NotInObject,
			NotInTeam,
			NotEnoughMP,
			NotEnoughItem,
			NotEnoughOffer,
			NotFriendlyHireFail,
			NoSkillToUninstall,
			NoSuchFaction,
			NoParameters,
			ObjectCantMove,
			ObjectIncomplete,
			ObjectIncompleteNamed,
			ObjectOwnerNotFriendly,
			OrderForLeader,
			OutOfDrugs,
			PasswordChanged,
			Patrols,
			PersonDrives,
			PersonOverloaded,
			Produces,
			Quitted,
			RadiationTooHighToEnter,
			RadiationOverdose,
			RadiationUnderdose,
			RadiationReceived,
			RadiationNotReceived,
			Receives,
			RefusesReceive,
			Rehired,
			Scavenges,
			Starved,
			TargetNotFriendly,
			TeamQuarrel,
			Trades,
			Uninstalls,
			UninstallsCollapsed,
			VehicleOverloaded
		}
	}

	public class HitEvent : ILocalized 
	{
		private static Hashtable _messages = null;
		private ILocalized _attacker;
		private ILocalized _defender;
		public ArrayList Results = new ArrayList();
		public int Hits = 0;

		public HitEvent(ILocalized attacker, ILocalized defender) 
		{
			_attacker = attacker;
			_defender = defender;
		}

		public string ToString(Lang lng) 
		{
			string s = _attacker.ToString(lng) + " -> " + _defender.ToString(lng) + ": ";
			for (int i = 0; i < Results.Count; i++)
			{
				if (i > 0)
					s += ", ";
				s += ((EventMessage)Messages[(Code)Results[i]]).ToString(lng);
			}

			if (Hits > 0) {
				if (Results.Count > 0)
					s += ", ";
				s += (lng == Lang.En) ? (Hits.ToString() + " hp inflicted") :
					("������ " + Hits.ToString() + " �����"); 
			}

			return s;
		}

		private static Hashtable Messages
		{
			get 
			{
				if (_messages == null) 
				{
					_messages = new Hashtable();
					_messages.Add(Code.NoHit, new EventMessage(
						"no hit", "������"));
					_messages.Add(Code.Block, new EventMessage(
						"block", "���� ����������"));
					_messages.Add(Code.ArmorSave, new EventMessage(
						"armor save", "������ �����"));
					_messages.Add(Code.NoWound, new EventMessage(
						"no wound", "��� �������"));
					_messages.Add(Code.Killed, new EventMessage(
						"killed", "�������� ����"));
					_messages.Add(Code.Exploded, new EventMessage(
						"object exploded", "������ �������"));
					_messages.Add(Code.Damaged, new EventMessage(
						"object damaged", "������ ��������"));
					_messages.Add(Code.Incapacitated, new EventMessage(
						"incapacitated", "�������� ������� �� �����"));
					_messages.Add(Code.CriticalHit, new EventMessage(
						"critical hit", "����. ���������"));
					_messages.Add(Code.CriticalBlock, new EventMessage(
						"critical block", "����. ����"));
					_messages.Add(Code.CriticalSave, new EventMessage(
						"critical save", "����. ������ �����"));
					_messages.Add(Code.CriticalWound, new EventMessage(
						"critical wound", "����. �������"));
					_messages.Add(Code.CriticalHitFail, new EventMessage(
						"critical hit fail", "����. ������"));
					_messages.Add(Code.CriticalBlockFail, new EventMessage(
						"critical block fail", "����. ������ �����"));
					_messages.Add(Code.CriticalSaveFail, new EventMessage(
						"critical save fail", "����. ������ �����"));
					_messages.Add(Code.CriticalWoundFail, new EventMessage(
						"critical wound fail", "����. ������ �������"));
				}
				return _messages;
			}
		}

		public enum Code
		{
			NoHit,
			Block,
			ArmorSave,
			NoWound,
			Killed,
			Exploded,
			Damaged,
			Incapacitated,
			CriticalHit,
			CriticalBlockFail,
			CriticalSaveFail,
			CriticalWound,
			CriticalHitFail,
			CriticalBlock,
			CriticalSave,
			CriticalWoundFail
		}
	}

	public class EventMessage : ILocalized 
	{
		private string _en;
		private string _ru;

		public EventMessage(string en, string ru) 
		{
			_en = en;
			_ru = ru;
		}

		public string ToString(Lang lng) 
		{
			if (lng == Lang.En)
				return _en;
			else
				return _ru;
		}
	}


	#region Lists

	public class EventList : ArrayList
	{
		public new Event this[int index] 
		{
			get { return (Event)base[index]; }
			set { base[index] = value; }
		}

		public override int Add(object value) 
		{
			throw new NotSupportedException();
		}

		public int Add(Event item) 
		{
			return base.Add(item);
		}

	}

	#endregion

}