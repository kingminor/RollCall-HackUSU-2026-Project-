import { PDFDocument, PDFTextField, PDFCheckBox } from 'pdf-lib';

const fileInput = document.getElementById('pdf_upload');

fileInput.addEventListener('change', async (e) => {
    const file = e.target.files[0];
    if (!file) return;

    const arrayBuffer = await file.arrayBuffer();
    const pdfDoc = await PDFDocument.load(arrayBuffer)
    const form = pdfDoc.getForm()
    const fields = form.getFields()

    console.log(arrayBuffer);
    console.log(pdfDoc);
    console.log(form);
    console.log(fields);

    const extractedData = {};

    fields.forEach(field => {
        const name = field.getName();
        let value = '';

        // Check by instance instead of constructor name string
        if (field instanceof PDFTextField) {
            value = field.getText();
        } else if (field instanceof PDFCheckBox) {
            value = field.isChecked();
        } else {
            // This helps you see if there are other types (dropdowns, etc)
            return;
        }

        extractedData[name] = value;
    });


    console.log(extractedData);
    const data = {
        name: extractedData[CharacterName],
        playerName: extractedData[PlayerName],
        level: sumLevelString(extractedData[ClassLevel]),
        race: extractedData[Race],
        class: extractedData[ClassLevel],
        background: extractedData[Background],
        alignment: extractedData[Alignment],
        str: extractedData[STR],
        dex: extractedData[DEX],
        con: extractedData[CON],
        int: extractedData[INT],
        wis: extractedData[WIS],
        cha: extractedData[CHA]
    }
})

/*
Character sheet object
{
    Background,
    CHA
    CON
    CharacterName
    ClassLevel
    DEX
    INT
    PlayerName
    Race
    STR
    WIS
    Alignment
}

*/

function sumLevelString(str) {
    if (!str) return 0;

    // Match all sequences of digits (e.g., "3", "12")
    const matches = str.match(/\d+/g);

    if (!matches) return 0;

    // Convert strings to numbers and sum them
    return matches.reduce((acc, curr) => acc + parseInt(curr, 10), 0);
}