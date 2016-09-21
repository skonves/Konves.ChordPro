//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.5.3
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\steve\Konves.ChordPro\src\Konves.ChordPro.Grammar\ChordProParser.g4 by ANTLR 4.5.3

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace Konves.ChordPro {
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System.Collections.Generic;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.5.3")]
[System.CLSCompliant(false)]
public partial class ChordProParser : Parser {
	public const int
		WS=1, HASH=2, OPEN_BRACE=3, OPEN_BRACKET=4, TEXT=5, LINE_BREAK=6, CLOSE_BRACKET=7, 
		COMMENT_TEXT=8, CLOSE_BRACE=9, COLON=10, NEW_SONG=11, TITLE=12, SUBTITLE=13, 
		COMMENT=14, COMMENT_ITALIC=15, COMMENT_BOX=16, START_OF_CHORUS=17, END_OF_CHORUS=18, 
		START_OF_TAB=19, END_OF_TAB=20, DEFINE=21, TEXTFONT=22, TEXTSIZE=23, CHORDFONT=24, 
		CHORDSIZE=25, CHORDCOLOR=26, NO_GRID=27, GRID=28, TITLES=29, NEW_PAGE=30, 
		NEW_PHYSICAL_PAGE=31, COLUMNS=32, COLUMN_BREAK=33, PAGETYPE=34;
	public const int
		RULE_document = 0, RULE_songLine = 1, RULE_block = 2, RULE_word = 3, RULE_syllable = 4, 
		RULE_chord = 5, RULE_directive = 6;
	public static readonly string[] ruleNames = {
		"document", "songLine", "block", "word", "syllable", "chord", "directive"
	};

	private static readonly string[] _LiteralNames = {
		null, null, "'#'", "'{'", null, null, null, "']'", null, null, "':'", 
		null, null, null, null, null, null, null, null, null, null, "'define'", 
		null, null, null, null, "'chordcolour'", null, null, "'titles'", null, 
		null, null, null, "'pagetype'"
	};
	private static readonly string[] _SymbolicNames = {
		null, "WS", "HASH", "OPEN_BRACE", "OPEN_BRACKET", "TEXT", "LINE_BREAK", 
		"CLOSE_BRACKET", "COMMENT_TEXT", "CLOSE_BRACE", "COLON", "NEW_SONG", "TITLE", 
		"SUBTITLE", "COMMENT", "COMMENT_ITALIC", "COMMENT_BOX", "START_OF_CHORUS", 
		"END_OF_CHORUS", "START_OF_TAB", "END_OF_TAB", "DEFINE", "TEXTFONT", "TEXTSIZE", 
		"CHORDFONT", "CHORDSIZE", "CHORDCOLOR", "NO_GRID", "GRID", "TITLES", "NEW_PAGE", 
		"NEW_PHYSICAL_PAGE", "COLUMNS", "COLUMN_BREAK", "PAGETYPE"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[System.Obsolete("Use Vocabulary instead.")]
	public static readonly string[] tokenNames = GenerateTokenNames(DefaultVocabulary, _SymbolicNames.Length);

	private static string[] GenerateTokenNames(IVocabulary vocabulary, int length) {
		string[] tokenNames = new string[length];
		for (int i = 0; i < tokenNames.Length; i++) {
			tokenNames[i] = vocabulary.GetLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = vocabulary.GetSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}

		return tokenNames;
	}

	[System.Obsolete]
	public override string[] TokenNames
	{
		get
		{
			return tokenNames;
		}
	}

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "ChordProParser.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string SerializedAtn { get { return _serializedATN; } }

	public ChordProParser(ITokenStream input)
		: base(input)
	{
		_interp = new ParserATNSimulator(this,_ATN);
	}
	public partial class DocumentContext : ParserRuleContext {
		public ITerminalNode Eof() { return GetToken(ChordProParser.Eof, 0); }
		public SongLineContext[] songLine() {
			return GetRuleContexts<SongLineContext>();
		}
		public SongLineContext songLine(int i) {
			return GetRuleContext<SongLineContext>(i);
		}
		public DirectiveContext[] directive() {
			return GetRuleContexts<DirectiveContext>();
		}
		public DirectiveContext directive(int i) {
			return GetRuleContext<DirectiveContext>(i);
		}
		public DocumentContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_document; } }
		public override void EnterRule(IParseTreeListener listener) {
			IChordProParserListener typedListener = listener as IChordProParserListener;
			if (typedListener != null) typedListener.EnterDocument(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IChordProParserListener typedListener = listener as IChordProParserListener;
			if (typedListener != null) typedListener.ExitDocument(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IChordProParserVisitor<TResult> typedVisitor = visitor as IChordProParserVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitDocument(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public DocumentContext document() {
		DocumentContext _localctx = new DocumentContext(_ctx, State);
		EnterRule(_localctx, 0, RULE_document);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 18;
			_errHandler.Sync(this);
			_la = _input.La(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << OPEN_BRACE) | (1L << OPEN_BRACKET) | (1L << TEXT))) != 0)) {
				{
				State = 16;
				switch (_input.La(1)) {
				case OPEN_BRACKET:
				case TEXT:
					{
					State = 14; songLine();
					}
					break;
				case OPEN_BRACE:
					{
					State = 15; directive();
					}
					break;
				default:
					throw new NoViableAltException(this);
				}
				}
				State = 20;
				_errHandler.Sync(this);
				_la = _input.La(1);
			}
			State = 21; Match(Eof);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class SongLineContext : ParserRuleContext {
		public BlockContext[] block() {
			return GetRuleContexts<BlockContext>();
		}
		public BlockContext block(int i) {
			return GetRuleContext<BlockContext>(i);
		}
		public ITerminalNode LINE_BREAK() { return GetToken(ChordProParser.LINE_BREAK, 0); }
		public ITerminalNode[] WS() { return GetTokens(ChordProParser.WS); }
		public ITerminalNode WS(int i) {
			return GetToken(ChordProParser.WS, i);
		}
		public SongLineContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_songLine; } }
		public override void EnterRule(IParseTreeListener listener) {
			IChordProParserListener typedListener = listener as IChordProParserListener;
			if (typedListener != null) typedListener.EnterSongLine(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IChordProParserListener typedListener = listener as IChordProParserListener;
			if (typedListener != null) typedListener.ExitSongLine(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IChordProParserVisitor<TResult> typedVisitor = visitor as IChordProParserVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitSongLine(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public SongLineContext songLine() {
		SongLineContext _localctx = new SongLineContext(_ctx, State);
		EnterRule(_localctx, 2, RULE_songLine);
		int _la;
		try {
			int _alt;
			EnterOuterAlt(_localctx, 1);
			{
			State = 23; block();
			State = 28;
			_errHandler.Sync(this);
			_alt = Interpreter.AdaptivePredict(_input,2,_ctx);
			while ( _alt!=2 && _alt!=global::Antlr4.Runtime.Atn.ATN.InvalidAltNumber ) {
				if ( _alt==1 ) {
					{
					{
					State = 24; Match(WS);
					State = 25; block();
					}
					} 
				}
				State = 30;
				_errHandler.Sync(this);
				_alt = Interpreter.AdaptivePredict(_input,2,_ctx);
			}
			State = 32;
			_la = _input.La(1);
			if (_la==WS) {
				{
				State = 31; Match(WS);
				}
			}

			State = 34; Match(LINE_BREAK);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class BlockContext : ParserRuleContext {
		public ChordContext chord() {
			return GetRuleContext<ChordContext>(0);
		}
		public ITerminalNode TEXT() { return GetToken(ChordProParser.TEXT, 0); }
		public WordContext word() {
			return GetRuleContext<WordContext>(0);
		}
		public BlockContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_block; } }
		public override void EnterRule(IParseTreeListener listener) {
			IChordProParserListener typedListener = listener as IChordProParserListener;
			if (typedListener != null) typedListener.EnterBlock(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IChordProParserListener typedListener = listener as IChordProParserListener;
			if (typedListener != null) typedListener.ExitBlock(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IChordProParserVisitor<TResult> typedVisitor = visitor as IChordProParserVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitBlock(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public BlockContext block() {
		BlockContext _localctx = new BlockContext(_ctx, State);
		EnterRule(_localctx, 4, RULE_block);
		try {
			State = 39;
			_errHandler.Sync(this);
			switch ( Interpreter.AdaptivePredict(_input,4,_ctx) ) {
			case 1:
				EnterOuterAlt(_localctx, 1);
				{
				State = 36; chord();
				}
				break;

			case 2:
				EnterOuterAlt(_localctx, 2);
				{
				State = 37; Match(TEXT);
				}
				break;

			case 3:
				EnterOuterAlt(_localctx, 3);
				{
				State = 38; word();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class WordContext : ParserRuleContext {
		public SyllableContext[] syllable() {
			return GetRuleContexts<SyllableContext>();
		}
		public SyllableContext syllable(int i) {
			return GetRuleContext<SyllableContext>(i);
		}
		public WordContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_word; } }
		public override void EnterRule(IParseTreeListener listener) {
			IChordProParserListener typedListener = listener as IChordProParserListener;
			if (typedListener != null) typedListener.EnterWord(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IChordProParserListener typedListener = listener as IChordProParserListener;
			if (typedListener != null) typedListener.ExitWord(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IChordProParserVisitor<TResult> typedVisitor = visitor as IChordProParserVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitWord(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public WordContext word() {
		WordContext _localctx = new WordContext(_ctx, State);
		EnterRule(_localctx, 6, RULE_word);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 42;
			_errHandler.Sync(this);
			_la = _input.La(1);
			do {
				{
				{
				State = 41; syllable();
				}
				}
				State = 44;
				_errHandler.Sync(this);
				_la = _input.La(1);
			} while ( _la==OPEN_BRACKET || _la==TEXT );
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class SyllableContext : ParserRuleContext {
		public ChordContext chord() {
			return GetRuleContext<ChordContext>(0);
		}
		public ITerminalNode TEXT() { return GetToken(ChordProParser.TEXT, 0); }
		public SyllableContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_syllable; } }
		public override void EnterRule(IParseTreeListener listener) {
			IChordProParserListener typedListener = listener as IChordProParserListener;
			if (typedListener != null) typedListener.EnterSyllable(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IChordProParserListener typedListener = listener as IChordProParserListener;
			if (typedListener != null) typedListener.ExitSyllable(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IChordProParserVisitor<TResult> typedVisitor = visitor as IChordProParserVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitSyllable(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public SyllableContext syllable() {
		SyllableContext _localctx = new SyllableContext(_ctx, State);
		EnterRule(_localctx, 8, RULE_syllable);
		try {
			State = 51;
			_errHandler.Sync(this);
			switch ( Interpreter.AdaptivePredict(_input,6,_ctx) ) {
			case 1:
				EnterOuterAlt(_localctx, 1);
				{
				State = 46; chord();
				}
				break;

			case 2:
				EnterOuterAlt(_localctx, 2);
				{
				State = 47; Match(TEXT);
				}
				break;

			case 3:
				EnterOuterAlt(_localctx, 3);
				{
				State = 48; chord();
				State = 49; Match(TEXT);
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ChordContext : ParserRuleContext {
		public ITerminalNode OPEN_BRACKET() { return GetToken(ChordProParser.OPEN_BRACKET, 0); }
		public ITerminalNode TEXT() { return GetToken(ChordProParser.TEXT, 0); }
		public ITerminalNode CLOSE_BRACKET() { return GetToken(ChordProParser.CLOSE_BRACKET, 0); }
		public ChordContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_chord; } }
		public override void EnterRule(IParseTreeListener listener) {
			IChordProParserListener typedListener = listener as IChordProParserListener;
			if (typedListener != null) typedListener.EnterChord(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IChordProParserListener typedListener = listener as IChordProParserListener;
			if (typedListener != null) typedListener.ExitChord(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IChordProParserVisitor<TResult> typedVisitor = visitor as IChordProParserVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitChord(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ChordContext chord() {
		ChordContext _localctx = new ChordContext(_ctx, State);
		EnterRule(_localctx, 10, RULE_chord);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 53; Match(OPEN_BRACKET);
			State = 54; Match(TEXT);
			State = 55; Match(CLOSE_BRACKET);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class DirectiveContext : ParserRuleContext {
		public ITerminalNode LINE_BREAK() { return GetToken(ChordProParser.LINE_BREAK, 0); }
		public ITerminalNode OPEN_BRACE() { return GetToken(ChordProParser.OPEN_BRACE, 0); }
		public ITerminalNode CLOSE_BRACE() { return GetToken(ChordProParser.CLOSE_BRACE, 0); }
		public ITerminalNode NEW_SONG() { return GetToken(ChordProParser.NEW_SONG, 0); }
		public ITerminalNode START_OF_CHORUS() { return GetToken(ChordProParser.START_OF_CHORUS, 0); }
		public ITerminalNode END_OF_CHORUS() { return GetToken(ChordProParser.END_OF_CHORUS, 0); }
		public ITerminalNode START_OF_TAB() { return GetToken(ChordProParser.START_OF_TAB, 0); }
		public ITerminalNode END_OF_TAB() { return GetToken(ChordProParser.END_OF_TAB, 0); }
		public ITerminalNode NO_GRID() { return GetToken(ChordProParser.NO_GRID, 0); }
		public ITerminalNode GRID() { return GetToken(ChordProParser.GRID, 0); }
		public ITerminalNode NEW_PAGE() { return GetToken(ChordProParser.NEW_PAGE, 0); }
		public ITerminalNode NEW_PHYSICAL_PAGE() { return GetToken(ChordProParser.NEW_PHYSICAL_PAGE, 0); }
		public ITerminalNode COLUMN_BREAK() { return GetToken(ChordProParser.COLUMN_BREAK, 0); }
		public ITerminalNode COLON() { return GetToken(ChordProParser.COLON, 0); }
		public ITerminalNode TEXT() { return GetToken(ChordProParser.TEXT, 0); }
		public ITerminalNode TITLE() { return GetToken(ChordProParser.TITLE, 0); }
		public ITerminalNode SUBTITLE() { return GetToken(ChordProParser.SUBTITLE, 0); }
		public ITerminalNode COMMENT() { return GetToken(ChordProParser.COMMENT, 0); }
		public ITerminalNode COMMENT_ITALIC() { return GetToken(ChordProParser.COMMENT_ITALIC, 0); }
		public ITerminalNode COMMENT_BOX() { return GetToken(ChordProParser.COMMENT_BOX, 0); }
		public ITerminalNode DEFINE() { return GetToken(ChordProParser.DEFINE, 0); }
		public ITerminalNode TEXTFONT() { return GetToken(ChordProParser.TEXTFONT, 0); }
		public ITerminalNode TEXTSIZE() { return GetToken(ChordProParser.TEXTSIZE, 0); }
		public ITerminalNode CHORDFONT() { return GetToken(ChordProParser.CHORDFONT, 0); }
		public ITerminalNode CHORDSIZE() { return GetToken(ChordProParser.CHORDSIZE, 0); }
		public ITerminalNode CHORDCOLOR() { return GetToken(ChordProParser.CHORDCOLOR, 0); }
		public ITerminalNode TITLES() { return GetToken(ChordProParser.TITLES, 0); }
		public ITerminalNode COLUMNS() { return GetToken(ChordProParser.COLUMNS, 0); }
		public ITerminalNode PAGETYPE() { return GetToken(ChordProParser.PAGETYPE, 0); }
		public DirectiveContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_directive; } }
		public override void EnterRule(IParseTreeListener listener) {
			IChordProParserListener typedListener = listener as IChordProParserListener;
			if (typedListener != null) typedListener.EnterDirective(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IChordProParserListener typedListener = listener as IChordProParserListener;
			if (typedListener != null) typedListener.ExitDirective(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IChordProParserVisitor<TResult> typedVisitor = visitor as IChordProParserVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitDirective(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public DirectiveContext directive() {
		DirectiveContext _localctx = new DirectiveContext(_ctx, State);
		EnterRule(_localctx, 12, RULE_directive);
		int _la;
		try {
			State = 69;
			_errHandler.Sync(this);
			switch ( Interpreter.AdaptivePredict(_input,7,_ctx) ) {
			case 1:
				EnterOuterAlt(_localctx, 1);
				{
				{
				State = 57; Match(OPEN_BRACE);
				State = 58;
				_la = _input.La(1);
				if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << NEW_SONG) | (1L << START_OF_CHORUS) | (1L << END_OF_CHORUS) | (1L << START_OF_TAB) | (1L << END_OF_TAB) | (1L << NO_GRID) | (1L << GRID) | (1L << NEW_PAGE) | (1L << NEW_PHYSICAL_PAGE) | (1L << COLUMN_BREAK))) != 0)) ) {
				_errHandler.RecoverInline(this);
				} else {
					Consume();
				}
				State = 59; Match(CLOSE_BRACE);
				}
				State = 61; Match(LINE_BREAK);
				}
				break;

			case 2:
				EnterOuterAlt(_localctx, 2);
				{
				{
				State = 62; Match(OPEN_BRACE);
				State = 63;
				_la = _input.La(1);
				if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << TITLE) | (1L << SUBTITLE) | (1L << COMMENT) | (1L << COMMENT_ITALIC) | (1L << COMMENT_BOX) | (1L << DEFINE) | (1L << TEXTFONT) | (1L << TEXTSIZE) | (1L << CHORDFONT) | (1L << CHORDSIZE) | (1L << CHORDCOLOR) | (1L << TITLES) | (1L << COLUMNS) | (1L << PAGETYPE))) != 0)) ) {
				_errHandler.RecoverInline(this);
				} else {
					Consume();
				}
				State = 64; Match(COLON);
				State = 65; Match(TEXT);
				State = 66; Match(CLOSE_BRACE);
				}
				State = 68; Match(LINE_BREAK);
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public static readonly string _serializedATN =
		"\x3\xAF6F\x8320\x479D\xB75C\x4880\x1605\x191C\xAB37\x3$J\x4\x2\t\x2\x4"+
		"\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b\x3\x2\x3\x2"+
		"\a\x2\x13\n\x2\f\x2\xE\x2\x16\v\x2\x3\x2\x3\x2\x3\x3\x3\x3\x3\x3\a\x3"+
		"\x1D\n\x3\f\x3\xE\x3 \v\x3\x3\x3\x5\x3#\n\x3\x3\x3\x3\x3\x3\x4\x3\x4\x3"+
		"\x4\x5\x4*\n\x4\x3\x5\x6\x5-\n\x5\r\x5\xE\x5.\x3\x6\x3\x6\x3\x6\x3\x6"+
		"\x3\x6\x5\x6\x36\n\x6\x3\a\x3\a\x3\a\x3\a\x3\b\x3\b\x3\b\x3\b\x3\b\x3"+
		"\b\x3\b\x3\b\x3\b\x3\b\x3\b\x3\b\x5\bH\n\b\x3\b\x2\x2\x2\t\x2\x2\x4\x2"+
		"\x6\x2\b\x2\n\x2\f\x2\xE\x2\x2\x4\a\x2\r\r\x13\x16\x1D\x1E !##\a\x2\xE"+
		"\x12\x17\x1C\x1F\x1F\"\"$$L\x2\x14\x3\x2\x2\x2\x4\x19\x3\x2\x2\x2\x6)"+
		"\x3\x2\x2\x2\b,\x3\x2\x2\x2\n\x35\x3\x2\x2\x2\f\x37\x3\x2\x2\x2\xEG\x3"+
		"\x2\x2\x2\x10\x13\x5\x4\x3\x2\x11\x13\x5\xE\b\x2\x12\x10\x3\x2\x2\x2\x12"+
		"\x11\x3\x2\x2\x2\x13\x16\x3\x2\x2\x2\x14\x12\x3\x2\x2\x2\x14\x15\x3\x2"+
		"\x2\x2\x15\x17\x3\x2\x2\x2\x16\x14\x3\x2\x2\x2\x17\x18\a\x2\x2\x3\x18"+
		"\x3\x3\x2\x2\x2\x19\x1E\x5\x6\x4\x2\x1A\x1B\a\x3\x2\x2\x1B\x1D\x5\x6\x4"+
		"\x2\x1C\x1A\x3\x2\x2\x2\x1D \x3\x2\x2\x2\x1E\x1C\x3\x2\x2\x2\x1E\x1F\x3"+
		"\x2\x2\x2\x1F\"\x3\x2\x2\x2 \x1E\x3\x2\x2\x2!#\a\x3\x2\x2\"!\x3\x2\x2"+
		"\x2\"#\x3\x2\x2\x2#$\x3\x2\x2\x2$%\a\b\x2\x2%\x5\x3\x2\x2\x2&*\x5\f\a"+
		"\x2\'*\a\a\x2\x2(*\x5\b\x5\x2)&\x3\x2\x2\x2)\'\x3\x2\x2\x2)(\x3\x2\x2"+
		"\x2*\a\x3\x2\x2\x2+-\x5\n\x6\x2,+\x3\x2\x2\x2-.\x3\x2\x2\x2.,\x3\x2\x2"+
		"\x2./\x3\x2\x2\x2/\t\x3\x2\x2\x2\x30\x36\x5\f\a\x2\x31\x36\a\a\x2\x2\x32"+
		"\x33\x5\f\a\x2\x33\x34\a\a\x2\x2\x34\x36\x3\x2\x2\x2\x35\x30\x3\x2\x2"+
		"\x2\x35\x31\x3\x2\x2\x2\x35\x32\x3\x2\x2\x2\x36\v\x3\x2\x2\x2\x37\x38"+
		"\a\x6\x2\x2\x38\x39\a\a\x2\x2\x39:\a\t\x2\x2:\r\x3\x2\x2\x2;<\a\x5\x2"+
		"\x2<=\t\x2\x2\x2=>\a\v\x2\x2>?\x3\x2\x2\x2?H\a\b\x2\x2@\x41\a\x5\x2\x2"+
		"\x41\x42\t\x3\x2\x2\x42\x43\a\f\x2\x2\x43\x44\a\a\x2\x2\x44\x45\a\v\x2"+
		"\x2\x45\x46\x3\x2\x2\x2\x46H\a\b\x2\x2G;\x3\x2\x2\x2G@\x3\x2\x2\x2H\xF"+
		"\x3\x2\x2\x2\n\x12\x14\x1E\").\x35G";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
} // namespace Konves.ChordPro
