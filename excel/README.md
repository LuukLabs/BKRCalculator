# BKR-rekentool voor Excel

`BKR-rekentool.xlsx` is een Excel-sjabloon dat dezelfde berekening doet als de
BKRCalculator-engine (`KDVManager.BKRCalculator.GroupAnalyzer`). Bedoeld als
hulpmiddel voor kinderopvangorganisaties die met Excel werken — de officiële
rekentool op [1ratio.nl](https://www.1ratio.nl/bkr/) blijft bindend.

## Gebruik

1. Open `BKR-rekentool.xlsx`.
2. Vul op het tabblad **Rekentool** het aantal kinderen per leeftijd in (geel).
3. Het minimum aantal beroepskrachten verschijnt direct bij **Benodigde beroepskrachten**.

Verschijnt *"Ongeldige groepssamenstelling"*, dan past geen enkele wettelijke
groepsnorm op die combinatie (bijvoorbeeld een te grote of niet-toegestane mengvorm).

> **Vereist Excel 2021 of Microsoft 365.** Het sjabloon gebruikt dynamische-array-
> evaluatie binnen `MATCH`; oudere versies (2016/2019) rekenen dit niet correct door.

## Hoe het werkt

- **Rekentool** — invoer en uitkomst.
- **Regels** — de wettelijke normentabel (leeftijdsband, max. groepsgrootte, sub-cap
  op nuljarigen, minimum beroepskrachten). Eén-op-één gelijk aan `AgeGroupRulesFactory`.
- **Berekening** (verborgen) — bootst de engine stap voor stap na: voor de groep zelf
  én voor de groep met telkens één kind minder per aanwezige leeftijd, zodat de
  extra-kracht-regel (één kind minder mag nooit méér beroepskrachten vereisen) klopt.

De ratio's per leeftijd (dagopvang, vanaf 1 juli 2024): 0 jr 1:3 · 1 jr 1:5 ·
2 jr 1:6 · 3 jr 1:8.

## Onderhoud

De regeltabel staat zowel in C# (`AgeGroupRulesFactory`) als in `generate_template.py`.
Wijzigen de regels? Werk beide bij en genereer het sjabloon opnieuw:

```bash
pip install openpyxl
python3 generate_template.py        # schrijft BKR-rekentool.xlsx
```

### Verificatie

De formules zijn op algoritme-niveau geverifieerd tegen de echte engine over
**alle 83.521 kind-combinaties** (0..16 per leeftijd) met **nul afwijkingen**.

```bash
# 1) Genereer ground truth uit de engine naar truth.csv (zie kop van
#    verify_against_engine.py voor het C#-programmaatje).
# 2) Vergelijk de Excel-logica ertegen:
python3 verify_against_engine.py truth.csv
# -> "FAITHFUL"
```

Dit dekt de rekenkundige logica. Het enige resterende risico is een Excel-
syntaxfout bij het genereren; controleer na wijzigingen één uitkomst in Excel zelf.
