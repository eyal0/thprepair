Module BinaryReaderBE
    <System.Runtime.CompilerServices.Extension()> _
    Public Function ReadUInt32BE(ByVal b As System.IO.BinaryReader) As UInt32
        Dim temp_bytes() As Byte = b.ReadBytes(4)

        If BitConverter.IsLittleEndian Then
            Array.Reverse(temp_bytes)
        End If
        Return BitConverter.ToUInt32(temp_bytes, 0)
    End Function

    <System.Runtime.CompilerServices.Extension()> _
    Public Function ReadSingleBE(ByVal b As System.IO.BinaryReader) As Single
        Dim temp_bytes() As Byte = b.ReadBytes(4)

        If BitConverter.IsLittleEndian Then
            Array.Reverse(temp_bytes)
        End If
        Return BitConverter.ToSingle(temp_bytes, 0)
    End Function
End Module
