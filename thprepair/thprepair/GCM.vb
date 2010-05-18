Public Module GCM
    'from gcmtool-0.3.2
    Private Const GCM_DISK_HEADER_OFFSET As Integer = 0
    Private Const GCM_FST_OFFSET_OFFSET As Integer = &H424

    Class GcmFile
        Property filename As String
        Property offset As Integer

        Public Overrides Function ToString() As String
            Return filename
        End Function
    End Class

    Structure GcmDir
        Dim dirname As String
        Dim recursive_file_count As Integer
    End Structure

    Public Function GetGcmFiles(ByVal br As System.IO.BinaryReader) As List(Of GcmFile)
        br.BaseStream.Position = GCM_DISK_HEADER_OFFSET + GCM_FST_OFFSET_OFFSET
        Dim fst_start_offset As Long = br.ReadUInt32BE
        br.BaseStream.Position = fst_start_offset + 8
        Dim file_count As UInt32 = br.ReadUInt32BE
        GetGcmFiles = New List(Of GcmFile)

        Dim current_path As New List(Of GcmDir)
        For i As Integer = 0 To CInt(file_count - 1)
            Do While current_path.Count > 0 AndAlso i >= current_path.Last.recursive_file_count
                current_path.RemoveAt(current_path.Count - 1)
            Loop

            'first the the location in the string table
            br.BaseStream.Position = fst_start_offset + i * 12
            Dim string_table_offset As Integer = CInt(br.ReadUInt32BE)
            If string_table_offset < &H1000000 Then 'it's a file
                Dim new_gcmfile As New GcmFile
                'add that to the start of the string table.  string table comes right after the file entries
                br.BaseStream.Position = fst_start_offset + file_count * 12 + string_table_offset
                new_gcmfile.filename = ""
                For Each d As GcmDir In current_path
                    new_gcmfile.filename &= d.dirname & "/"
                Next
                'now read until a null char
                Do While br.ReadByte <> 0
                    br.BaseStream.Seek(-1, IO.SeekOrigin.Current)
                    new_gcmfile.filename &= Chr(br.ReadByte)
                Loop

                'now the file_offset
                br.BaseStream.Position = fst_start_offset + i * 12 + 4
                new_gcmfile.offset = CInt(br.ReadUInt32BE)
                GetGcmFiles.Add(new_gcmfile)
            Else 'if it's a directory
                string_table_offset = string_table_offset And &HFFFFFF
                Dim new_gcmdir As New GcmDir
                If i = 0 Then
                    new_gcmdir.dirname = ""
                Else
                    br.BaseStream.Position = fst_start_offset + file_count * 12 + string_table_offset
                    'now read until a null char
                    new_gcmdir.dirname = ""
                    Do While br.PeekChar <> 0
                        new_gcmdir.dirname &= br.ReadChar
                    Loop
                End If

                'now the recursive file count
                br.BaseStream.Position = fst_start_offset + i * 12 + 8
                new_gcmdir.recursive_file_count = CInt(br.ReadUInt32BE) 'actually just one beyond the end of the list
                current_path.Add(new_gcmdir)
            End If
        Next
    End Function

End Module
