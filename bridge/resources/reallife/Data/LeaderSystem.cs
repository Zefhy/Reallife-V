﻿using GTANetworkAPI;
using reallife.Player;
using System;
using System.Collections.Generic;
using System.Text;

namespace reallife.Data
{
    class LeaderSystem
    {
        public static int GetRank(Client client)
        {
            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(client);

            if (playerInfo == null)
                return -1;

            return playerInfo.fleader;
        }

        public static bool HasRank(Client client, int rank)
        {
            int currentRank = GetRank(client);

            if (currentRank < rank || currentRank > rank)
                return false;

            return true;
        }

        public static bool IsLeader(Client client, int rank)
        {
            int currentRank = GetRank(client);

            if (currentRank < rank)
                return false;

            return true;
        }

        public static bool Same(Client client, Client player)
        {
            PlayerInfo clientInfo = PlayerHelper.GetPlayerStats(client);
            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(player);

            if (clientInfo.fleader < playerInfo.fraktion || clientInfo.fleader > playerInfo.fraktion)
                return false;

            return true;
        }

        public static bool SetRank(Client client, string playerName, int rank)
        {
            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(client);

            if (playerInfo == null)
                return false;

            if (playerInfo.fleader <= rank)
                return false;

            Client target = NAPI.Player.GetPlayerFromName(playerName);

            if (target == null)
                return false;

            PlayerInfo targetInfo = PlayerHelper.GetPlayerStats(target);

            if (targetInfo == null)
                return false;

            targetInfo.fleader = rank;
            targetInfo.Update();
            return true;
        }
    }
}
