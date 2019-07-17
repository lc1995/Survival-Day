import csv
import json

defends_filename = "C:\\Users\\49899\\Desktop\\Defends.csv"
attacks_filename = "C:\\Users\\49899\\Desktop\\Attacks.csv"
json_filename = "C:\\Users\\49899\\Desktop\\Actions.json"
final = dict()


# Defends
defends = dict()
with open(defends_filename) as f:
    reader = csv.DictReader(f)

    for row in reader:
        key = row['id']
        if key in defends:
            defends[key]['results'].append({
                'description' : str(row['result']),
                'probability' : float(row['probability']),
                'atkFactor' : float(row['atkFactor']),
                'bounceFactor' : float(row['bounceFactor'])
            })
        else:
            defends[key] = dict()
            defends[key] = {
                'id' : int(row['id']),
                'type' : int(row['type']),
                'description' : str(row['description'])
            }
            defends[key]['results'] = [{
                'description' : str(row['result']),
                'probability' : float(row['probability']),
                'atkFactor' : float(row['atkFactor']),
                'bounceFactor' : float(row['bounceFactor'])
            }]

defends = list(defends.values())
final['defends'] = defends

# Attacks
attacks = list()
with open(attacks_filename) as f:
    reader = csv.DictReader(f)

    for row in reader:
        attacks.append({
            'id' : int(row['id']),
            'type' : int(row['type']),
            'description' : str(row['description']),
            'baseDamage' : float(row['baseDamage'])
        })

final['attacks'] = attacks

with open(json_filename, "w") as f:
    f.write(json.dumps(final, indent=2))
    
