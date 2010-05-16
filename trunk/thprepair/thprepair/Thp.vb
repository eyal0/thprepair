Public Class Thp
    'http://www.amnoid.de/gc/thp.txt

    Class ThpHeader
        Public tag() As Char '"THP\0"
        Public Const VERSION_1_1 As UInt32 = &H11000
        Public Const VERSION_1_0 As UInt32 = &H10000
        Public version As UInt32 '0x00011000 = 1.1, 0x00010000 = 1.0
        Public maxBufferSize As UInt32
        Public maxAudioSamples As UInt32
        Public fps As Single
        Public numFrames As UInt32
        Public firstFrameSize As UInt32
        Public dataSize As UInt32
        Public componentDataOffset As UInt32
        Public offsetsDataOffset As UInt32
        Public firstFrameOffset As UInt32
        Public lastFrameOffset As UInt32

        Shared Function Read(ByVal f As System.IO.BinaryReader) As ThpHeader
            Read = New ThpHeader
            Read.tag = f.ReadChars(4)
            Read.version = f.ReadUInt32BE
            Read.maxBufferSize = f.ReadUInt32BE
            Read.maxAudioSamples = f.ReadUInt32BE
            Read.fps = f.ReadSingleBE
            Read.numFrames = f.ReadUInt32BE
            Read.firstFrameSize = f.ReadUInt32BE
            Read.dataSize = f.ReadUInt32BE
            Read.componentDataOffset = f.ReadUInt32BE
            Read.offsetsDataOffset = f.ReadUInt32BE
            Read.firstFrameOffset = f.ReadUInt32BE
            Read.lastFrameOffset = f.ReadUInt32BE
        End Function

        Sub Repair()
        End Sub
    End Class

    Class ThpComponentsHeader
        Enum ComponentType As Byte
            Video = 0
            Audio = 1
        End Enum
        Public numComponents As UInt32
        Public componentTypes(0 To 15) As ComponentType

        Shared Function Read(ByVal f As System.IO.BinaryReader) As ThpComponentsHeader
            Read = New ThpComponentsHeader
            Read.numComponents = f.ReadUInt32BE
            For i As Integer = 0 To 15 'always 16 numbers, even if numComponents is less than 16
                Read.componentTypes(i) = CType(f.ReadByte, ComponentType)
            Next
        End Function

        Sub Repair()
        End Sub
    End Class

    MustInherit Class ThpComponent
        MustOverride Sub Repair()
    End Class

    Class ThpVideoInfo
        Inherits ThpComponent
        Public width As UInt32
        Public height As UInt32
        Public unknown As UInt32 'only for version 1.1 thp files

        Shared Function Read(ByVal f As System.IO.BinaryReader, ByVal version As UInt32) As ThpVideoInfo
            Read = New ThpVideoInfo
            Read.width = f.ReadUInt32BE
            Read.height = f.ReadUInt32BE
            If version = ThpHeader.VERSION_1_1 Then
                Read.unknown = f.ReadUInt32BE
            End If
        End Function

        Public Overrides Sub Repair()
        End Sub
    End Class

    Class ThpAudioInfo
        Inherits ThpComponent
        Public numChannels As UInt32
        Public frequency As UInt32
        Public numSamples As UInt32
        Public numData As UInt32 'only for version 1.1 - that many

        Shared Function Read(ByVal f As System.IO.BinaryReader, ByVal version As UInt32) As ThpAudioInfo
            Read = New ThpAudioInfo
            Read.numChannels = f.ReadUInt32BE
            Read.frequency = f.ReadUInt32BE
            Read.numSamples = f.ReadUInt32BE
            If version = ThpHeader.VERSION_1_1 Then
                Read.numData = f.ReadUInt32BE
            End If
        End Function

        Public Overrides Sub Repair()
        End Sub
    End Class

    Class ThpFrame
        Class ThpFrameHeader
            Public nextTotalSize As UInt32
            Public prevTotalSize As UInt32
            Public imageSize As UInt32
            Public audioSize As UInt32 'only if the file contains audio

            Shared Function Read(ByVal f As System.IO.BinaryReader, ByVal containsAudio As Boolean) As ThpFrameHeader
                Read = New ThpFrameHeader
                Read.nextTotalSize = f.ReadUInt32BE
                Read.prevTotalSize = f.ReadUInt32BE
                Read.imageSize = f.ReadUInt32BE
                If containsAudio Then
                    Read.audioSize = f.ReadUInt32BE
                End If
            End Function

            Sub Repair()
            End Sub
        End Class

        Class ThpVideoFrame
            Public data() As Byte
            Public fixed_data() As Byte
            Public image As Image

            Shared Function Read(ByVal f As System.IO.BinaryReader, ByVal imageSize As UInt32) As ThpVideoFrame
                Read = New ThpVideoFrame
                Read.data = f.ReadBytes(CInt(imageSize))
            End Function

            Sub Repair()
                Dim current_byte As Integer = 0
                Dim temp_fixed_data As New List(Of Byte) 'with ff converted to ff 00 as needed
                Dim end_byte As Integer = data.Length - 2
                Do While data(end_byte) <> &HFF AndAlso data(end_byte + 1) <> &HD9
                    end_byte -= 1
                Loop
                Do While data(current_byte) <> &HFF OrElse data(current_byte + 1) <> &HDA
                    temp_fixed_data.Add(data(current_byte))
                    current_byte += 1
                Loop
                temp_fixed_data.Add(data(current_byte))
                current_byte += 1
                temp_fixed_data.Add(data(current_byte))
                current_byte += 1
                Do While current_byte < end_byte
                    temp_fixed_data.Add(data(current_byte))
                    If data(current_byte) = &HFF Then
                        temp_fixed_data.Add(&H0)
                    End If
                    current_byte += 1
                Loop
                Do While current_byte < data.Length
                    temp_fixed_data.Add(data(current_byte))
                    current_byte += 1
                Loop
                fixed_data = temp_fixed_data.ToArray
                Dim ms As New System.IO.MemoryStream(fixed_data, False)
                Try
                    image = image.FromStream(ms)
                Catch ex As Exception
                    Throw New ImageException("Can't make image", ex)
                End Try
            End Sub

            Class ImageException
                Inherits ApplicationException
                Sub New(ByVal message As String, ByVal innerException As Exception)
                    MyBase.New(message, innerException)
                End Sub
            End Class
        End Class

        Class ThpAudioFrame
            Public data() As Byte 'no parsing for now

            Shared Function Read(ByVal f As System.IO.BinaryReader, ByVal audioSize As Long) As ThpAudioFrame
                Read = New ThpAudioFrame
                Read.data = f.ReadBytes(CInt(audioSize)) 'no parsing for now
            End Function

            Sub Repair()
            End Sub
        End Class

        Public MyThpFrameHeader As ThpFrameHeader
        Public MyThpVideoFrame As ThpVideoFrame
        Public MyThpAudioFrame As ThpAudioFrame
        Public MyPadding() As Byte
        Shared Function Read(ByVal f As System.IO.BinaryReader, ByVal audioNumData As Integer, ByVal frameSize As UInt32) As ThpFrame
            Read = New ThpFrame
            Read.MyThpFrameHeader = ThpFrameHeader.Read(f, audioNumData > 0)
            Read.MyThpVideoFrame = ThpVideoFrame.Read(f, Read.MyThpFrameHeader.imageSize)
            Read.MyThpAudioFrame = ThpAudioFrame.Read(f, audioNumData * Read.MyThpFrameHeader.audioSize)
            If frameSize - (Read.MyThpFrameHeader.imageSize + audioNumData * Read.MyThpFrameHeader.audioSize + 16) > 0 Then
                Read.MyPadding = f.ReadBytes(CInt(frameSize - (Read.MyThpFrameHeader.imageSize + audioNumData * Read.MyThpFrameHeader.audioSize + 16)))
            End If
        End Function

        Sub Repair()
            MyThpFrameHeader.Repair()
            MyThpVideoFrame.Repair()
            MyThpAudioFrame.Repair()
        End Sub
    End Class

    Public MyThpHeader As ThpHeader
    Public MyThpComponentsHeader As ThpComponentsHeader
    Public MyThpComponents As List(Of ThpComponent)
    Public MyThpFrames As List(Of ThpFrame)

    Public Shared Function Read(ByVal f As System.IO.BinaryReader) As Thp
        Read = New Thp
        Read.MyThpHeader = ThpHeader.Read(f)
        Read.MyThpComponentsHeader = ThpComponentsHeader.Read(f)
        Read.MyThpComponents = New List(Of ThpComponent)
        For i As Integer = 0 To CInt(Read.MyThpComponentsHeader.numComponents - 1)
            If Read.MyThpComponentsHeader.componentTypes(i) = ThpComponentsHeader.ComponentType.Video Then
                Read.MyThpComponents.Add(ThpVideoInfo.Read(f, Read.MyThpHeader.version))
            ElseIf Read.MyThpComponentsHeader.componentTypes(i) = ThpComponentsHeader.ComponentType.Audio Then
                Read.MyThpComponents.Add(ThpAudioInfo.Read(f, Read.MyThpHeader.version))
            Else
                Throw New Exception("Unrecognized Thp Component Type: " & Read.MyThpComponentsHeader.componentTypes(i))
            End If
        Next
        Read.MyThpFrames = New List(Of ThpFrame)
        For i As Integer = 0 To CInt(Read.MyThpHeader.numFrames - 1)
            Dim currentFrameSize As UInt32
            If i = 0 Then
                currentFrameSize = Read.MyThpHeader.firstFrameSize
            Else
                currentFrameSize = Read.MyThpFrames(i - 1).MyThpFrameHeader.nextTotalSize
            End If
            If Read.MyThpComponentsHeader.componentTypes.Length < 2 Then
                Read.MyThpFrames.Add(ThpFrame.Read(f, 0, currentFrameSize))
            Else
                Read.MyThpFrames.Add(ThpFrame.Read(f, CInt(CType(Read.MyThpComponents(1), ThpAudioInfo).numData), currentFrameSize))
            End If
        Next
    End Function

    Sub Repair()
        MyThpHeader.Repair()
        MyThpComponentsHeader.Repair()
        For Each tc As ThpComponent In MyThpComponents
            tc.Repair()
        Next
        For i As Integer = 0 To MyThpFrames.Count - 1
            Try
                MyThpFrames(i).Repair()
            Catch ex As ThpFrame.ThpVideoFrame.ImageException
                'find a frame nearby that is smaller
                Dim current_frame As Integer = i - 1
                If MyThpFrames(current_frame).MyThpFrameHeader.imageSize <= MyThpFrames(i).MyThpFrameHeader.imageSize Then
                    Array.Clear(MyThpFrames(i).MyThpVideoFrame.data, 0, MyThpFrames(i).MyThpVideoFrame.data.Length)
                    Array.Copy(MyThpFrames(current_frame).MyThpVideoFrame.data,
                               MyThpFrames(i).MyThpVideoFrame.data,
                               MyThpFrames(current_frame).MyThpVideoFrame.data.Length)
                End If
            End Try
        Next
    End Sub
End Class
