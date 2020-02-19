using _19_lab;
using System;
using System.Collections.Generic;
using System.Text;

namespace _19_lab_role_game
{
        interface Magic
        {
            void performMagicEffect(CreateCharacter.Mage caster, CreateCharacter.Person aim, int border);
        }
}
