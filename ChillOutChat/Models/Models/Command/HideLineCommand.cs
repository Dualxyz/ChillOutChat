///////////////////////////////////////////////////////////
//  HideLineCommand.cs
//  Implementation of the Class HideLineCommand
//  Generated by Enterprise Architect
//  Created on:      04-jun-2023 09:37:08
//  Original author: Andrej
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



using chatpkg;
namespace chatpkg {
	public class HideLineCommand : LineCommand {

		public HideLineCommand(){

		}

        public HideLineCommand(ChatRoom chatRoom, ChatLine chatLine) : base(chatRoom, chatLine)
        {
        }

        ~HideLineCommand(){

		}

		public override void Execute(){
            chatRoom.RemoveChatLine(chatLine);
		}

		public override void UnExecute(){
            chatRoom.AddChatLine(chatLine);
		}

	}//end HideLineCommand

}//end namespace chatpkg