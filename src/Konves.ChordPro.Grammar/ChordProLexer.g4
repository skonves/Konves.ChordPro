lexer grammar ChordProLexer;

// Start of line
WS: [ \t\r\n]+ -> skip; // chuck all preceding whitespace

HASH: '#' -> skip, pushMode(LINECOMMENT);

OPEN_BRACE : '{' -> pushMode(DIRECTIVE);

OPEN_BRACKET: '[' -> pushMode(SONGLINE);
TEXT: ~[ #\{\r\n\[\]]+ -> pushMode(SONGLINE);

mode SONGLINE;
LINE_BREAK: [ \t]* [\r\n]+ -> popMode;
SONGLINE_WS: [ \t]+ -> type(WS);

SONGLINE_OPEN_BRACKET: '[' -> type(OPEN_BRACKET);
CLOSE_BRACKET: ']';

SONGLINE_TEXT: ~[ \{\r\n\[\]]+ -> type(TEXT);

mode LINECOMMENT;
COMMENT_TEXT: ~[\r\n]+ -> skip;
COMMENT_LINE_BREAK: [\r\n]+ -> type(LINE_BREAK), popMode, skip;

mode DIRECTIVE;
DIRECTIVE_LINE_BREAK: [ \t]* [\r\n]+ -> type(LINE_BREAK), popMode;

CLOSE_BRACE : '}' ;
COLON: ':' -> pushMode(ARGUMENT);

// Preamble
NEW_SONG: 'new_song' | 'ns';
TITLE: 'title' | 't';
SUBTITLE: 'subtitle' | 'st';

// Formatting
COMMENT: 'comment' | 'c';
COMMENT_ITALIC: 'comment_italic' | 'ci';
COMMENT_BOX: 'comment_box' | 'cb';
START_OF_CHORUS: 'start_of_chorus' | 'soc';
END_OF_CHORUS: 'end_of_chorus' | 'eoc';
START_OF_TAB: 'start_of_tab' | 'eot';
END_OF_TAB: 'end_of_tab' | 'eot';
DEFINE: 'define';

// Output Related
TEXTFONT: 'textfont' | 'tf';
TEXTSIZE: 'textsize' | 'ts';
CHORDFONT: 'chordfont' | 'cf';
CHORDSIZE: 'chordsize' | 'cs';
CHORDCOLOR: 'chordcolour';
NO_GRID: 'no_grid' | 'ng';
GRID: 'grid' | 'g';
TITLES: 'titles';
NEW_PAGE: 'new_page' | 'np';
NEW_PHYSICAL_PAGE: 'new_physical_page' | 'npp';
COLUMNS: 'columns' | 'col';
COLUMN_BREAK: 'column_break' | 'colb';
PAGETYPE: 'pagetype';

mode ARGUMENT;
ARGUMENT_TEXT: ~[\}\r\n]+ -> type(TEXT);
ARGUMENT_CLOSE_BRACE : '}' -> type(CLOSE_BRACE), popMode ;
