﻿Imports System.Windows.Forms
Imports SFML.Audio

Module ClientSound
    Public FadeInSwitch As Boolean
    Public FadeOutSwitch As Boolean
    Public CurMusic As String

    Sub PlayMusic(ByVal FileName As String)
        If Not Options.Music = 1 Or Not FileExist(Application.StartupPath & MUSIC_PATH & FileName) Then Exit Sub
        If FileName = CurMusic Then Exit Sub

        If MusicPlayer Is Nothing Then
            Try
                MusicPlayer = New Music(Application.StartupPath & MUSIC_PATH & FileName)
                MusicPlayer.Loop() = True
                MusicPlayer.Volume() = 0
                MusicPlayer.Play()
                CurMusic = FileName
                FadeInSwitch = True
            Catch ex As Exception

            End Try
        Else
            Try
                FadeOutSwitch = True
                'MusicPlayer.Stop()
                'MusicPlayer.Dispose()
                'MusicPlayer = Nothing
                'MusicPlayer = New Music(Application.StartupPath & MUSIC_PATH & FileName)
                'MusicPlayer.Loop() = True
                'MusicPlayer.Play()
            Catch ex As Exception

            End Try
        End If
    End Sub

    Sub StopMusic()
        If MusicPlayer Is Nothing Then Exit Sub
        MusicPlayer.Stop()
        MusicPlayer.Dispose()
        MusicPlayer = Nothing
    End Sub

    Sub PlaySound(ByVal FileName As String)
        If Not Options.Sound = 1 Or Not FileExist(Application.StartupPath & SOUND_PATH & FileName) Then Exit Sub
        Dim buffer As SoundBuffer
        If SoundPlayer Is Nothing Then
            SoundPlayer = New Sound()
            buffer = New SoundBuffer(Application.StartupPath & SOUND_PATH & FileName)
            SoundPlayer.SoundBuffer = buffer
            SoundPlayer.Play()
        Else
            SoundPlayer.Stop()
            buffer = New SoundBuffer(Application.StartupPath & SOUND_PATH & FileName)
            SoundPlayer.SoundBuffer = buffer
            SoundPlayer.Play()
        End If
    End Sub

    Sub StopSound()
        If SoundPlayer Is Nothing Then Exit Sub
        SoundPlayer.Dispose()
        SoundPlayer = Nothing
    End Sub

    Sub FadeIn()
        If MusicPlayer.Volume() = 100 Then FadeInSwitch = False
        MusicPlayer.Volume() = MusicPlayer.Volume() + 1
    End Sub

    Sub FadeOut()
        If MusicPlayer.Volume() = 0 Then
            FadeOutSwitch = False
            If Map.Music = Nothing Then
                StopMusic()
            Else
                PlayMusic(Map.Music)
            End If
        End If

        MusicPlayer.Volume() = MusicPlayer.Volume() - 1
    End Sub
End Module
