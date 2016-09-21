lexer grammar ChordProLexer;

OPEN_BRACE : '{' -> pushMode(DIRECTIVE);

LINE_BREAK: [\r\n]+;
WS: [ \t]+;

OPEN_BRACKET: '[';
CLOSE_BRACKET: ']';

TEXT: ~[ \{\r\n\[\]]+;

mode DIRECTIVE;

CLOSE_BRACE : '}' -> popMode ;

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
ARGUMENT_TEXT: ~[\}\r\n]+;
CLOSE_BRACE_2 : '}' -> popMode, popMode ;
