import csv
import json

def output_Attack():
    attacks_filename = "/Users/cesare/Desktop/Attack.csv"
    attacks_json = "/Users/cesare/Desktop/Attack.json"

    final = dict()
    attacks = list()
    with open(attacks_filename) as f:
        reader = csv.DictReader(f)
        for row in reader:
            # print row

            attacks.append({
                'id'            : int(row['\xef\xbb\xbfID']),
                'choice'        : str(row['Choice']),
                'description'   : str(row['Description']),
                'substitude'    : [int(s) for s in row['Substitude'].split('|') if s.isdigit()],
                'type'          : [int(s) for s in row['Type'].split('|') if s.isdigit()],
                'phyDamage'     : int(row['phyDamage']),
                'magDamage'     : int(row['magDamage']),
                'precision'     : int(row['Precision']),
                'ammo'          : int(row['AmmoID']),
                'condition'     : int(row['Condition']),
                'cd'            : int(row['CD']),
                'objGenerated'  : int(row['objGenerated']),
                'strength'      : float(row['Strength']),
                'magic'         : float(row['Magic']),
                'agility'       : float(row['Agility'])
            })
    final['attacks'] = attacks
    with open(attacks_json, "w") as f:
        f.write(json.dumps(final, indent=2))

def output_Defense_Choice():
    defense_choice = "/Users/cesare/Desktop/Defense_choice.csv"
    defense_c_json =  "/Users/cesare/Desktop/Defense_choice.json"

    final = dict()
    attacks = list()
    counter = 0
    with open(defense_choice) as f:
        reader = csv.DictReader(f)
        for row in reader:
            counter+=1
            print counter
            attacks.append({
                'id'            : int(row['\xef\xbb\xbfID']),
                'type'          : int(row['Type']),
                'defend'        : str(row['Defend']),
                'condition'     : int(row['Condition']),
                'result'        : [int(s) for s in row['Result'].split('|') if s.isdigit()]
            })
    final['defense_choice'] = attacks
    with open(defense_c_json, "w") as f:
        f.write(json.dumps(final, indent=2))
			
def output_Defense_Result():
    defense_result = "/Users/cesare/Desktop/Defense_result.csv"
    defense_r_json =  "/Users/cesare/Desktop/Defense_result.json"

    final = dict()
    attacks = list()
    counter = 0
    with open(defense_result) as f:
        reader = csv.DictReader(f)
        for row in reader:
            # counter+=1
            # print counter
            attacks.append({
                'id'            : int(row['\xef\xbb\xbfID']),
                'description'   : str(row['ResultDescription']),
                'substitude'    : [int(s) for s in row['Substitude'].split('|') if s.isdigit()],
                'param'         : int(row['Param']),
                'aglParam'      : float(row['aglParam']),
                'weapParam'     : float(row['weapParam']),
                'actParam'      : float(row['actParam']),
                'atkFactor'     : float(row['atkFactor']),
                'ctkFactor'     : float(row['ctkFactor']),
            })
    final['defense_choice'] = attacks
    with open(defense_r_json, "w") as f:
        f.write(json.dumps(final, indent=2))   

if __name__ == '__main__':
    # output_Attack()
    # output_Defense_Choice()
    output_Defense_Result()