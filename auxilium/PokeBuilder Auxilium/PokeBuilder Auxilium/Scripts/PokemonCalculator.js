var items;
var pokemons;
var natures;
var moves;
var abilities;
var pokemonsmoves;
var pokemonsabilities;
var typeEfficiacy;


var selectedItem;
var selectedPokemon;
var selectedNature;

var selectedAbility;
var selectedMoves;

function startUp() {
    loadLists();
}
//onChange
function pokemonChange() {
    selectedPokemon = document.getElementById('Pokemon').value;

    htmlString = "";
    pokemonsabilities.forEach(function (item) {
        if (item.pokemonID == selectedPokemon) {
            abilities.forEach(function (a) {
                if (item.abilityID == a.abilityID) {
                    htmlString += "<option value=" + a.abilityID + ">" + a.abilityName + "</option>";
                }
            });
            document.getElementById("Ability").innerHTML = htmlString;
        }
    });
    htmlString = "";
    pokemonsmoves.forEach(function (item) {
        if (item.pokemonID == selectedPokemon) {
            moves.forEach(function (a) {
                if (item.moveID == a.moveID) {
                    htmlString += "<option value=" + a.moveID + ">" + a.moveName + "</option>";
                }
            });
            document.getElementById("Move").innerHTML = htmlString;
        }
    });
    calc();
}
//onCHange
function pokemonDefChange() {
    selectedPokemon = document.getElementById('PokemonDef').value;

    htmlString = "";
    pokemonsabilities.forEach(function (item) {
        if (item.pokemonID == selectedPokemon) {
            abilities.forEach(function (a) {
                if (item.abilityID == a.abilityID) {
                    htmlString += "<option value=" + a.abilityID + ">" + a.abilityName + "</option>";
                }
            });
            document.getElementById("AbilityDef").innerHTML = htmlString;
        }
    });
    calc();
}
//JSONREQUEST
function loadLists() {
    $.getJSON('pokedex/GetItems', {}, function (res) {
        items = res;
        items.sort(sortByImportance);

        htmlString = "";
        items.forEach(function (item) {
            
            htmlString += "<option value=" + item.itemID + ">" + item.itemName + "</option>";
        });
        document.getElementById('Item').innerHTML = htmlString;
        document.getElementById('ItemDef').innerHTML = htmlString;
    });
    $.getJSON('pokedex/GetPokemons', {}, function (res) {
        pokemons = res;
        pokemons.sort(sortByTier);

        htmlString = "<option></option>";
        pokemons.forEach(function (item) {
            
            htmlString += "<option value=" + item.pokemonID + ">" + item.pokemonName + "</option>";
        });
        document.getElementById('Pokemon').innerHTML = htmlString;
        document.getElementById('PokemonDef').innerHTML = htmlString;
    });
    $.getJSON('pokedex/GetNatures', {}, function (res) {
        natures = res;

        htmlString = "";
        natures.forEach(function (item) {
            htmlString += "<option value=" + item.nature + ">" + item.nature + " (+" + item.statBuffed + ", -" + item.statNerfed + ") </option>";
        });
        document.getElementById('Nature').innerHTML = htmlString;
        document.getElementById('NatureDef').innerHTML = htmlString;
    });
    $.getJSON('pokedex/GetMoves', {}, function (res) {
        moves = res;
    });
    $.getJSON('pokedex/GetAbilities', {}, function (res) {
        abilities = res;
    });
    $.getJSON('pokedex/GetPokemonsMoves', {}, function (res) {
        pokemonsmoves = res;
    });
    $.getJSON('pokedex/GetPokemonsAbilities', {}, function (res) {
        pokemonsabilities = res;
    });
    $.getJSON('pokedex/GetTypeEfficiacy', {}, function (res) {
        typeEfficiacy = res;
    })
}
//SortierMethode
sortByImportance = function (a, b) {
    if (a.itemImportant) {
        if (b.itemImportant) {
            if (a.itemName < b.itemName) {
                return -1;
            }
            else {
                return 1;
            }
        }
        else {
            return -1;
        }
    }
    if (b.itemImportant) {
        return 1;
    }
    if (a.itemName < b.itemName) {
        return -1;
    }
    else {
        return 1;
    }
};
//SortierMethode
sortByTier = function (a, b) {
    if (a.tier == "OU") {
        return -1;
    } else if (a.tier == "UUBL") {
        if (b.tier == "OU") {
            return 1;
        }
        else {
            return -1;
        }
    }
    else if (a.tier == "UU") {
        if (b.tier == "OU" || b.tier == "UUBL") {
            return 1;
        }
        else {
            return -1;
        }
    } else if (a.tier == "NUBL") {
        if (b.tier == "OU" || b.tier == "UUBL" || b.tier == "UU") {
            return 1;
        }
        else {
            return -1;
        }
    }
    else if (a.tier == "NU") {
        if (b.tier == "OU" || b.tier == "UUBL" || b.tier == "UU" || b.tier == "NUBL") {
            return 1;
        }
        else {
            return -1;
        }
    }
    else {
        return 1;
    }
};

$(document).ready(startUp());



//Forme zur Berechnung des Schadens
function calc() {

    document.getElementById('result').innerHTML = "";

    attacker = pokemons.filter(function (value) {
        return value.pokemonID == document.getElementById('Pokemon').value;
    })[0];
    defender= pokemons.filter(function (value) {
        return value.pokemonID == document.getElementById('PokemonDef').value;
    })[0];

    Level =1*document.getElementById("lvl").value||50;
    DvHP = document.getElementById('DvHP').value||0;
    DvAtk = document.getElementById('DvAtk').value || 0;
    DvDef = document.getElementById('DvDef').value || 0;
    DvSpA = document.getElementById('DvSpA').value || 0;
    DvSpD = document.getElementById('DvSpD').value || 0;
    DvSpe = document.getElementById('DvSpe').value || 0;

    EvHP = document.getElementById('EvHP').value || 0;
    EvAtk = document.getElementById('EvAtk').value || 0;
    EvDef = document.getElementById('EvDef').value || 0;
    EvSpA = document.getElementById('EvSpA').value || 0;
    EvSpD = document.getElementById('EvSpD').value || 0;
    EvSpe = document.getElementById('EvSpe').value || 0;

    DLevel = 1*document.getElementById("Dlvl").value || 50;
    DDvHP = document.getElementById('DDvHP').value || 0;
    DDvAtk = document.getElementById('DDvAtk').value || 0;
    DDvDef = document.getElementById('DDvDef').value || 0;
    DDvSpA = document.getElementById('DDvSpA').value || 0;
    DDvSpD = document.getElementById('DDvSpD').value || 0;
    DDvSpe = document.getElementById('DDvSpe').value || 0;

    DEvHP = document.getElementById('DEvHP').value || 0;
    DEvAtk = document.getElementById('DEvAtk').value || 0;
    DEvDef = document.getElementById('DEvDef').value || 0;
    DEvSpA = document.getElementById('DEvSpA').value || 0;
    DEvSpD = document.getElementById('DEvSpD').value || 0;
    DEvSpe = document.getElementById('DEvSpe').value || 0;

    nature = natures.filter(function (value) {
        return value.nature == document.getElementById('Nature').value;
    })[0];
    natatk = 1;
    natdef = 1;
    natspa = 1;
    natspd = 1;
    natspe = 1;
    switch (nature.statBuffed) {
        case "Atk":
            natatk = natatk + 0.1;
            break;
        case "Def":
            natdef = natdef + 0.1;
            break;
        case "SpA":
            natspa = natspa + 0.1;
            break;
        case "SpD":
            natspd = natspd + 0.1;
            break;
        case "Spe":
            natspe = natspe + 0.1;
            break;
    }
    switch (nature.statNerfed) {
        case "Atk":
            natatk = natatk - 0.1;
            break;
        case "Def":
            natdef = natdef - 0.1;
            break;
        case "SpA":
            natspa = natspa - 0.1;
            break;
        case "SpD":
            natspd = natspd - 0.1;
            break;
        case "Spe":
            natspe = natspe - 0.1;
            break;
    }
    dNature = natures.filter(function (value) {
        return value.nature == document.getElementById('Nature').value;
    })[0];
    dNatatk = 1;
    dNatdef = 1;
    dNatspa = 1;
    dNatspd = 1;
    dNatspe = 1;
    switch (dNature.statBuffed) {
        case "Atk":
            dNatatk = dNatatk + 0.1;
            break;
        case "Def":
            dNatdef = dNatdef + 0.1;
            break;
        case "SpA":
            dNatspa = dNatspa + 0.1;
            break;
        case "SpD":
            dNatspd = dNatspd + 0.1;
            break;
        case "Spe":
            dNatspe = dNatspe + 0.1;
            break;
    }
    switch (dNature.statNerfed) {
        case "Atk":
            dNatatk = dNatatk - 0.1;
            break;
        case "Def":
            dNatdef = dNatdef - 0.1;
            break;
        case "SpA":
            dNatspa = dNatspa - 0.1;
            break;
        case "SpD":
            dNatspd = dNatspd - 0.1;
            break;
        case "Spe":
            dNatspe = dNatspe - 0.1;
            break;
    }

   
    maxhp = ((2 * attacker.stats[0].statValue + parseInt(DvHP) + parseInt(EvHP) / 4.0) * Level) / 100 + Level + 10.0;
    hp = maxhp;
    atk = ((2.0 * attacker.stats[1].statValue + parseInt(DvAtk) + parseInt(EvAtk) / 4.0) * Level / 100.0 + 5.0) * natatk;
    def = ((2.0 * attacker.stats[2].statValue + parseInt(DvDef) + parseInt(EvDef) / 4.0) * Level / 100.0 + 5.0) * natdef;
    spa = ((2.0 * attacker.stats[3].statValue + parseInt(DvSpA) + parseInt(EvSpA) / 4.0) * Level / 100.0 + 5.0) * natspa;
    spd = ((2.0 * attacker.stats[4].statValue + parseInt(DvSpD) + parseInt(EvSpD) / 4.0) * Level / 100.0 + 5.0) * natspd;
    spe = ((2.0 * attacker.stats[5].statValue + parseInt(DvSpe) + parseInt(EvSpe) / 4.0) * Level / 100.0 + 5.0) * natspe;

    maxhpDefender = ((2 * defender.stats[0].statValue + parseInt(DDvHP) + parseInt(DEvHP) / 4.0) * DLevel) / 100 + DLevel + 10.0;
    hpDefender = maxhpDefender;
    atkDefender = ((2.0 * defender.stats[1].statValue + parseInt(DDvAtk) + parseInt(DEvAtk) / 4.0) * DLevel / 100.0 + 5.0) * dNatatk;
    defDefender = ((2.0 * defender.stats[2].statValue + parseInt(DDvDef) + parseInt(DEvDef) / 4.0) * DLevel / 100.0 + 5.0) * dNatdef;
    spaDefender = ((2.0 * defender.stats[3].statValue + parseInt(DDvSpA) + parseInt(DEvSpA) / 4.0) * DLevel / 100.0 + 5.0) * dNatspa;
    spdDefender = ((2.0 * defender.stats[4].statValue + parseInt(DDvSpD) + parseInt(DEvSpD) / 4.0) * DLevel / 100.0 + 5.0) * dNatspd;
    speDefender = ((2.0 * defender.stats[5].statValue + parseInt(DDvSpe) + parseInt(DEvSpe) / 4.0) * DLevel / 100.0 + 5.0) * dNatspe;

    
    if (document.getElementById('Stealth').checked) {
        stealth = 0.25;
        if (defender.type[0] == 3 || defender.type[0] == 7 || defender.type[0] == 10 || defender.type[0] == 15) {
            stealth *= 2;
        }
        else if (defender.type[0] == 2 || defender.type[0] == 5 || defender.type[0] == 9) {
            stealth /= 2;
        }
        if (defender.type[1] == 3 || defender.type[1] == 7 || defender.type[1] == 10 || defender.type[1] == 15) {
            stealth *= 2;
        }
        else if (defender.type[1] == 2 || defender.type[1] == 5 || defender.type[1] == 9) {
            stealth /= 2;
        }
        hpDefender = hpDefender * (1 - stealth);
    }
    if (document.getElementById('spike1').checked && ability.abilityID != 26 && defender.type[0]!=3) {//Levitate
        hpDefender = hpDefender * 0.875;
    } else if (document.getElementById('spike2').checked && ability.abilityID != 26 && defender.type[0] != 3) {
        hpDefender = hpDefender * 0.75;
    } else if (document.getElementById('spike3').checked && ability.abilityID != 26 && defender.type[0] != 3) {
        hpDefender = hpDefender * 0.625;
    }

    move = moves.filter(function (value) {
        return value.moveID == document.getElementById('Move').value;
    })[0];
    
    AttackDmg = move.moveStrength;


    AttackStat = 1;
    DefenseStat = 1;

    screens = 1;
    doubleMult = 1;
    if (document.getElementById('double').checked) {
        doubleMult = 0.75;
    }


    //1 Status 2 Physical 3 Special
    if (move.moveID == 492)//Foul Play
    {
        AttackStat = atkDefender;
        DefenseStat = defDefender;
        if (document.getElementById('reflector').checked) {
            if (doubleMult < 1) {
                screens = 0.666666666666666;
            }
            else {
                screens = 0.5;
            }
        }
    }
    else if (move.moveID ==473 ||move.moveID == 540||move.moveID==548)//Psyshock / Psystrike / Secret Sword
    {
        AttackStat = spa;
        DefenseStat = defDefender;
        if (document.getElementById('reflector').checked) {
            if (doubleMult < 1) {
                screens = 0.666666666666666;
            }
            else {
                screens = 0.5;
            }
        }
    }
    else if(move.damageType == 1) {
        return;
    }
    else if (move.damageType == 2) {
        AttackStat = atk;
        DefenseStat = defDefender;
        if (document.getElementById('reflector').checked) {
            if (doubleMult<1) {
                screens = 0.666666666666666;
            }
            else {
                screens = 0.5;
            }
        }
    }
    else {
        AttackStat = spa;
        DefenseStat = spdDefender;
        if (document.getElementById('lightscreen').checked) {
            if (doubleMult<1) {
                screens = 0.666666666666666;
            }
            else {
                screens = 0.5;
            }
        }
    }

    Crit = 1;

    STAB = 1;
    if (move.moveType == attacker.type1 || move.moveType == attacker.type2) {
        Stab = 1.5;
    }
    typeMult = 1;
    typeEfficiacy.forEach(function (item) {
        if (defender.type[0] == item.def && move.moveType == item.atk) {
            typeMult = typeMult * item.multiplier;
        }
        if (defender.type[1] == item.def && move.moveType == item.atk) {
            typeMult = typeMult * item.multiplier;
        }
    });

    burn = 1;
    if (document.getElementById('burn').checked == true && move.damageType==2) {
        burn = 0.5;
    }
    weather = 1;
    if ((move.moveType == 11 && document.getElementById('rain').checked) || (move.moveType == 10 && document.getElementById('sun').checked)) {
        weather = 1.5;
    }
    if (move.moveID == 280)//brick break
    {
        screens = 1;
    }
    Factor1 = burn * screens * doubleMult * weather;

    item = items.filter(function (value) {
        return value.itemID == document.getElementById('Item').value;
    })[0];
    lifeOrb = 1;
    if (item.itemID == 247) {//Life Orb
        lifeOrb = 1.3;
    }
    Factor2 = lifeOrb;

    abilityDef = abilities.filter(function (value) {
        return value.abilityID == document.getElementById('AbilityDef').value;
    })[0];
    ability = abilities.filter(function (value) {
        return value.abilityID == document.getElementById('Ability').value;
    })[0]; 

    fsr = 1;
    if (abilityDef.abilityID == 111 || abilityDef.abilityID == 116) {//Filter, Solid  Rock
        fsr = 0.5;
    }
    expert = 1;
    if (item.itemID == 245) {//Expert Belt
        expert=1.2
    }
    tl = 1;
    if (ability.abilityID == 110) {//Tinted Lens
        tl = 2;
    }

    Factor3 = fsr * expert * tl;

    DamageInHpMinRNG = (((Level * 2 / 5 + 2) * AttackDmg * AttackStat / (50 * DefenseStat)) * Factor1 + 2) * Crit * Factor2 * 0.85 * STAB * typeMult * Factor3;
    DamageInHpMaxRNG = (((Level * 2 / 5 + 2) * AttackDmg * AttackStat / (50 * DefenseStat)) * Factor1 + 2) * Crit * Factor2 * 1 * STAB * typeMult * Factor3;
    if (move.moveID ==220)//PainSplit
    {
        DamageInHpMinRNG = (hp + hpDefender) / 2;
        DamageInHpMaxRNG = (hp + hpDefender) / 2;
    }
    DamagePercentMinRNG = 100 * DamageInHpMinRNG / hpDefender;
    DamagePercentMaxRNG = 100 * DamageInHpMaxRNG / hpDefender;

    RNGToKill = hpDefender / ((((Level * 2 / 5 + 2) * AttackDmg * AttackStat / (50 * DefenseStat)) * Factor1 + 2) * Crit * Factor2 * STAB * typeMult * Factor3);
    
    document.getElementById('result').innerHTML = "";
    document.getElementById('result').innerHTML += "<span>" + attacker.pokemonName + " does a minimum of " + parseInt(DamageInHpMinRNG) + "HP and a maximum of " + parseInt(DamageInHpMaxRNG) + "HP on " + defender.pokemonName + " (HP:" + maxhpDefender + ")</span>";
    document.getElementById('result').innerHTML += "<br /><br />";
    document.getElementById('result').innerHTML += "<span>That's a minimum of " + DamagePercentMinRNG.toFixed(2) + "% and a maximum of " + DamagePercentMaxRNG.toFixed(2) + "% of </span><span style='color:blue'>" + defender.pokemonName + "'s</span><span> current Health</span>";
    if (RNGToKill > 0.85 && RNGToKill <= 1) {
        ohkoPercent = 100*((1 - RNGToKill) / 0.15);
        document.getElementById('result').innerHTML += "<br /><br />";
        document.getElementById('result').innerHTML += "<span> The OHKO Chance is " + ohkoPercent.toFixed(2) + "% </span>";
    }

}












        /*Pokemon Damage Formel


            ((Level * 2 / 5 + 2) * Attack dmg * Sp.Atk / (50 * Sp.Def) * Factor1 + 2) * CritMultiplier * Factor 2 * rng / 100 * STAB multiplier * Type1 multiplier * Type2 multiplier * Factor3

    Factor 1 = Burn Multiplier * Screens Multiplier * AOE Debuff * Weather Dmg Multiplier * Flash Fire Multiplier

    Factor 2 = Me First Multiplier * Life Orb Multiplier * Metronom Multiplier

    Factor 3 = Solid Rock Multiplier * Filter Multiplier * Tinted Lens Multiplier

        * INFO

    Split Up  ->  rng=100
    FOUL Play -> Defender Atk
    Brick Break ignores Screens
    Psyshock / Psystrike / Secret Sword -> Sp Atk on Physical Defense
    Sacred Sword ignores Defense Buffs
    Pain Split splits hp
    Night Shade and Seismic Toss always does Damage equivelent of Users Level
    Endeavor makes Defenders Health to Attackers Health
    Super Fang and Natures Madness half the Current HP of Defender
    Guillotine, Horn Drill, Sheer Cold and Fissure Insta KO except Defender has Sash or Sturdy
*/
