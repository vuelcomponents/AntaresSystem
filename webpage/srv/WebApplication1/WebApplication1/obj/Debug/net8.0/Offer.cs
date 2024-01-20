// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: offer.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from offer.proto</summary>
public static partial class OfferReflection {

  #region Descriptor
  /// <summary>File descriptor for offer.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static OfferReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "CgtvZmZlci5wcm90bxoNY29tcGFueS5wcm90bxoOcG9zaXRpb24ucHJvdG8a",
          "DmRvY3VtZW50LnByb3RvGhFyZWNydWl0bWVudC5wcm90byLGAQoFT2ZmZXIS",
          "CgoCaWQYASABKAMSDAoEY29kZRgCIAEoCRINCgV0aXRsZRgDIAEoCRIPCgdt",
          "ZXNzYWdlGAQgASgJEhkKB2NvbXBhbnkYBSABKAsyCC5Db21wYW55EhsKCHBv",
          "c2l0aW9uGAYgASgLMgkuUG9zaXRpb24SGAoFaW1hZ2UYByABKAsyCS5Eb2N1",
          "bWVudBIhCgtyZWNydWl0bWVudBgIIAEoCzIMLlJlY3J1aXRtZW50Eg4KBmds",
          "b2JhbBgJIAEoCCIaCgxPZmZlclJlcXVlc3QSCgoCaWQYASABKAMiJgoNT2Zm",
          "ZXJSZXNwb25zZRIVCgVvZmZlchgBIAEoCzIGLk9mZmVyMj0KEE9mZmVyR3Jw",
          "Y1NlcnZpY2USKQoIR2V0T2ZmZXISDS5PZmZlclJlcXVlc3QaDi5PZmZlclJl",
          "c3BvbnNlYgZwcm90bzM="));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { global::Protos.CompanyReflection.Descriptor, global::PositionReflection.Descriptor, global::DocumentReflection.Descriptor, global::RecruitmentReflection.Descriptor, },
        new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
          new pbr::GeneratedClrTypeInfo(typeof(global::Offer), global::Offer.Parser, new[]{ "Id", "Code", "Title", "Message", "Company", "Position", "Image", "Recruitment", "Global" }, null, null, null, null),
          new pbr::GeneratedClrTypeInfo(typeof(global::OfferRequest), global::OfferRequest.Parser, new[]{ "Id" }, null, null, null, null),
          new pbr::GeneratedClrTypeInfo(typeof(global::OfferResponse), global::OfferResponse.Parser, new[]{ "Offer" }, null, null, null, null)
        }));
  }
  #endregion

}
#region Messages
[global::System.Diagnostics.DebuggerDisplayAttribute("{ToString(),nq}")]
public sealed partial class Offer : pb::IMessage<Offer>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , pb::IBufferMessage
#endif
{
  private static readonly pb::MessageParser<Offer> _parser = new pb::MessageParser<Offer>(() => new Offer());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pb::MessageParser<Offer> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::OfferReflection.Descriptor.MessageTypes[0]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public Offer() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public Offer(Offer other) : this() {
    id_ = other.id_;
    code_ = other.code_;
    title_ = other.title_;
    message_ = other.message_;
    company_ = other.company_ != null ? other.company_.Clone() : null;
    position_ = other.position_ != null ? other.position_.Clone() : null;
    image_ = other.image_ != null ? other.image_.Clone() : null;
    recruitment_ = other.recruitment_ != null ? other.recruitment_.Clone() : null;
    global_ = other.global_;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public Offer Clone() {
    return new Offer(this);
  }

  /// <summary>Field number for the "id" field.</summary>
  public const int IdFieldNumber = 1;
  private long id_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public long Id {
    get { return id_; }
    set {
      id_ = value;
    }
  }

  /// <summary>Field number for the "code" field.</summary>
  public const int CodeFieldNumber = 2;
  private string code_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public string Code {
    get { return code_; }
    set {
      code_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "title" field.</summary>
  public const int TitleFieldNumber = 3;
  private string title_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public string Title {
    get { return title_; }
    set {
      title_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "message" field.</summary>
  public const int MessageFieldNumber = 4;
  private string message_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public string Message {
    get { return message_; }
    set {
      message_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "company" field.</summary>
  public const int CompanyFieldNumber = 5;
  private global::Protos.Company company_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public global::Protos.Company Company {
    get { return company_; }
    set {
      company_ = value;
    }
  }

  /// <summary>Field number for the "position" field.</summary>
  public const int PositionFieldNumber = 6;
  private global::Position position_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public global::Position Position {
    get { return position_; }
    set {
      position_ = value;
    }
  }

  /// <summary>Field number for the "image" field.</summary>
  public const int ImageFieldNumber = 7;
  private global::Document image_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public global::Document Image {
    get { return image_; }
    set {
      image_ = value;
    }
  }

  /// <summary>Field number for the "recruitment" field.</summary>
  public const int RecruitmentFieldNumber = 8;
  private global::Recruitment recruitment_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public global::Recruitment Recruitment {
    get { return recruitment_; }
    set {
      recruitment_ = value;
    }
  }

  /// <summary>Field number for the "global" field.</summary>
  public const int GlobalFieldNumber = 9;
  private bool global_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool Global {
    get { return global_; }
    set {
      global_ = value;
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override bool Equals(object other) {
    return Equals(other as Offer);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool Equals(Offer other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (Id != other.Id) return false;
    if (Code != other.Code) return false;
    if (Title != other.Title) return false;
    if (Message != other.Message) return false;
    if (!object.Equals(Company, other.Company)) return false;
    if (!object.Equals(Position, other.Position)) return false;
    if (!object.Equals(Image, other.Image)) return false;
    if (!object.Equals(Recruitment, other.Recruitment)) return false;
    if (Global != other.Global) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override int GetHashCode() {
    int hash = 1;
    if (Id != 0L) hash ^= Id.GetHashCode();
    if (Code.Length != 0) hash ^= Code.GetHashCode();
    if (Title.Length != 0) hash ^= Title.GetHashCode();
    if (Message.Length != 0) hash ^= Message.GetHashCode();
    if (company_ != null) hash ^= Company.GetHashCode();
    if (position_ != null) hash ^= Position.GetHashCode();
    if (image_ != null) hash ^= Image.GetHashCode();
    if (recruitment_ != null) hash ^= Recruitment.GetHashCode();
    if (Global != false) hash ^= Global.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void WriteTo(pb::CodedOutputStream output) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    output.WriteRawMessage(this);
  #else
    if (Id != 0L) {
      output.WriteRawTag(8);
      output.WriteInt64(Id);
    }
    if (Code.Length != 0) {
      output.WriteRawTag(18);
      output.WriteString(Code);
    }
    if (Title.Length != 0) {
      output.WriteRawTag(26);
      output.WriteString(Title);
    }
    if (Message.Length != 0) {
      output.WriteRawTag(34);
      output.WriteString(Message);
    }
    if (company_ != null) {
      output.WriteRawTag(42);
      output.WriteMessage(Company);
    }
    if (position_ != null) {
      output.WriteRawTag(50);
      output.WriteMessage(Position);
    }
    if (image_ != null) {
      output.WriteRawTag(58);
      output.WriteMessage(Image);
    }
    if (recruitment_ != null) {
      output.WriteRawTag(66);
      output.WriteMessage(Recruitment);
    }
    if (Global != false) {
      output.WriteRawTag(72);
      output.WriteBool(Global);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
    if (Id != 0L) {
      output.WriteRawTag(8);
      output.WriteInt64(Id);
    }
    if (Code.Length != 0) {
      output.WriteRawTag(18);
      output.WriteString(Code);
    }
    if (Title.Length != 0) {
      output.WriteRawTag(26);
      output.WriteString(Title);
    }
    if (Message.Length != 0) {
      output.WriteRawTag(34);
      output.WriteString(Message);
    }
    if (company_ != null) {
      output.WriteRawTag(42);
      output.WriteMessage(Company);
    }
    if (position_ != null) {
      output.WriteRawTag(50);
      output.WriteMessage(Position);
    }
    if (image_ != null) {
      output.WriteRawTag(58);
      output.WriteMessage(Image);
    }
    if (recruitment_ != null) {
      output.WriteRawTag(66);
      output.WriteMessage(Recruitment);
    }
    if (Global != false) {
      output.WriteRawTag(72);
      output.WriteBool(Global);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(ref output);
    }
  }
  #endif

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public int CalculateSize() {
    int size = 0;
    if (Id != 0L) {
      size += 1 + pb::CodedOutputStream.ComputeInt64Size(Id);
    }
    if (Code.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(Code);
    }
    if (Title.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(Title);
    }
    if (Message.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(Message);
    }
    if (company_ != null) {
      size += 1 + pb::CodedOutputStream.ComputeMessageSize(Company);
    }
    if (position_ != null) {
      size += 1 + pb::CodedOutputStream.ComputeMessageSize(Position);
    }
    if (image_ != null) {
      size += 1 + pb::CodedOutputStream.ComputeMessageSize(Image);
    }
    if (recruitment_ != null) {
      size += 1 + pb::CodedOutputStream.ComputeMessageSize(Recruitment);
    }
    if (Global != false) {
      size += 1 + 1;
    }
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(Offer other) {
    if (other == null) {
      return;
    }
    if (other.Id != 0L) {
      Id = other.Id;
    }
    if (other.Code.Length != 0) {
      Code = other.Code;
    }
    if (other.Title.Length != 0) {
      Title = other.Title;
    }
    if (other.Message.Length != 0) {
      Message = other.Message;
    }
    if (other.company_ != null) {
      if (company_ == null) {
        Company = new global::Protos.Company();
      }
      Company.MergeFrom(other.Company);
    }
    if (other.position_ != null) {
      if (position_ == null) {
        Position = new global::Position();
      }
      Position.MergeFrom(other.Position);
    }
    if (other.image_ != null) {
      if (image_ == null) {
        Image = new global::Document();
      }
      Image.MergeFrom(other.Image);
    }
    if (other.recruitment_ != null) {
      if (recruitment_ == null) {
        Recruitment = new global::Recruitment();
      }
      Recruitment.MergeFrom(other.Recruitment);
    }
    if (other.Global != false) {
      Global = other.Global;
    }
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(pb::CodedInputStream input) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    input.ReadRawMessage(this);
  #else
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 8: {
          Id = input.ReadInt64();
          break;
        }
        case 18: {
          Code = input.ReadString();
          break;
        }
        case 26: {
          Title = input.ReadString();
          break;
        }
        case 34: {
          Message = input.ReadString();
          break;
        }
        case 42: {
          if (company_ == null) {
            Company = new global::Protos.Company();
          }
          input.ReadMessage(Company);
          break;
        }
        case 50: {
          if (position_ == null) {
            Position = new global::Position();
          }
          input.ReadMessage(Position);
          break;
        }
        case 58: {
          if (image_ == null) {
            Image = new global::Document();
          }
          input.ReadMessage(Image);
          break;
        }
        case 66: {
          if (recruitment_ == null) {
            Recruitment = new global::Recruitment();
          }
          input.ReadMessage(Recruitment);
          break;
        }
        case 72: {
          Global = input.ReadBool();
          break;
        }
      }
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
          break;
        case 8: {
          Id = input.ReadInt64();
          break;
        }
        case 18: {
          Code = input.ReadString();
          break;
        }
        case 26: {
          Title = input.ReadString();
          break;
        }
        case 34: {
          Message = input.ReadString();
          break;
        }
        case 42: {
          if (company_ == null) {
            Company = new global::Protos.Company();
          }
          input.ReadMessage(Company);
          break;
        }
        case 50: {
          if (position_ == null) {
            Position = new global::Position();
          }
          input.ReadMessage(Position);
          break;
        }
        case 58: {
          if (image_ == null) {
            Image = new global::Document();
          }
          input.ReadMessage(Image);
          break;
        }
        case 66: {
          if (recruitment_ == null) {
            Recruitment = new global::Recruitment();
          }
          input.ReadMessage(Recruitment);
          break;
        }
        case 72: {
          Global = input.ReadBool();
          break;
        }
      }
    }
  }
  #endif

}

[global::System.Diagnostics.DebuggerDisplayAttribute("{ToString(),nq}")]
public sealed partial class OfferRequest : pb::IMessage<OfferRequest>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , pb::IBufferMessage
#endif
{
  private static readonly pb::MessageParser<OfferRequest> _parser = new pb::MessageParser<OfferRequest>(() => new OfferRequest());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pb::MessageParser<OfferRequest> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::OfferReflection.Descriptor.MessageTypes[1]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public OfferRequest() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public OfferRequest(OfferRequest other) : this() {
    id_ = other.id_;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public OfferRequest Clone() {
    return new OfferRequest(this);
  }

  /// <summary>Field number for the "id" field.</summary>
  public const int IdFieldNumber = 1;
  private long id_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public long Id {
    get { return id_; }
    set {
      id_ = value;
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override bool Equals(object other) {
    return Equals(other as OfferRequest);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool Equals(OfferRequest other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (Id != other.Id) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override int GetHashCode() {
    int hash = 1;
    if (Id != 0L) hash ^= Id.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void WriteTo(pb::CodedOutputStream output) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    output.WriteRawMessage(this);
  #else
    if (Id != 0L) {
      output.WriteRawTag(8);
      output.WriteInt64(Id);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
    if (Id != 0L) {
      output.WriteRawTag(8);
      output.WriteInt64(Id);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(ref output);
    }
  }
  #endif

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public int CalculateSize() {
    int size = 0;
    if (Id != 0L) {
      size += 1 + pb::CodedOutputStream.ComputeInt64Size(Id);
    }
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(OfferRequest other) {
    if (other == null) {
      return;
    }
    if (other.Id != 0L) {
      Id = other.Id;
    }
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(pb::CodedInputStream input) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    input.ReadRawMessage(this);
  #else
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 8: {
          Id = input.ReadInt64();
          break;
        }
      }
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
          break;
        case 8: {
          Id = input.ReadInt64();
          break;
        }
      }
    }
  }
  #endif

}

[global::System.Diagnostics.DebuggerDisplayAttribute("{ToString(),nq}")]
public sealed partial class OfferResponse : pb::IMessage<OfferResponse>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , pb::IBufferMessage
#endif
{
  private static readonly pb::MessageParser<OfferResponse> _parser = new pb::MessageParser<OfferResponse>(() => new OfferResponse());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pb::MessageParser<OfferResponse> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::OfferReflection.Descriptor.MessageTypes[2]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public OfferResponse() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public OfferResponse(OfferResponse other) : this() {
    offer_ = other.offer_ != null ? other.offer_.Clone() : null;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public OfferResponse Clone() {
    return new OfferResponse(this);
  }

  /// <summary>Field number for the "offer" field.</summary>
  public const int OfferFieldNumber = 1;
  private global::Offer offer_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public global::Offer Offer {
    get { return offer_; }
    set {
      offer_ = value;
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override bool Equals(object other) {
    return Equals(other as OfferResponse);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool Equals(OfferResponse other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (!object.Equals(Offer, other.Offer)) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override int GetHashCode() {
    int hash = 1;
    if (offer_ != null) hash ^= Offer.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void WriteTo(pb::CodedOutputStream output) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    output.WriteRawMessage(this);
  #else
    if (offer_ != null) {
      output.WriteRawTag(10);
      output.WriteMessage(Offer);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
    if (offer_ != null) {
      output.WriteRawTag(10);
      output.WriteMessage(Offer);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(ref output);
    }
  }
  #endif

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public int CalculateSize() {
    int size = 0;
    if (offer_ != null) {
      size += 1 + pb::CodedOutputStream.ComputeMessageSize(Offer);
    }
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(OfferResponse other) {
    if (other == null) {
      return;
    }
    if (other.offer_ != null) {
      if (offer_ == null) {
        Offer = new global::Offer();
      }
      Offer.MergeFrom(other.Offer);
    }
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(pb::CodedInputStream input) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    input.ReadRawMessage(this);
  #else
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 10: {
          if (offer_ == null) {
            Offer = new global::Offer();
          }
          input.ReadMessage(Offer);
          break;
        }
      }
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
          break;
        case 10: {
          if (offer_ == null) {
            Offer = new global::Offer();
          }
          input.ReadMessage(Offer);
          break;
        }
      }
    }
  }
  #endif

}

#endregion


#endregion Designer generated code
