parser grammar ChordProParser;
options { tokenVocab=ChordProLexer; }

document: (songLine | directive)* EOF ;

songLine: block(WS block)* LINE_BREAK;

block: chord | TEXT | word;

word: syllable+;

syllable: chord | TEXT | chord TEXT ;

chord: OPEN_BRACKET TEXT CLOSE_BRACKET;

//songWord: ;

//chord: OPEN_BRACKET CHORD_TEXT CLOSE_BRACKET ;

directive:
	(OPEN_BRACE (
		NEW_SONG |
		START_OF_CHORUS |
		END_OF_CHORUS |
		START_OF_TAB |
		END_OF_TAB |
		NO_GRID |
		GRID | 
		NEW_PAGE |
		NEW_PHYSICAL_PAGE |
		COLUMN_BREAK )
	CLOSE_BRACE) LINE_BREAK |
	(OPEN_BRACE (
		TITLE |
		SUBTITLE |
		COMMENT |
		COMMENT_ITALIC |
		COMMENT_BOX |
		DEFINE |
		TEXTFONT |
		TEXTSIZE |
		CHORDFONT |
		CHORDSIZE |
		CHORDCOLOR |
		TITLES |
		COLUMNS |
		PAGETYPE )
	COLON TEXT CLOSE_BRACE) LINE_BREAK;
