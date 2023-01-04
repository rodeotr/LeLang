using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.Learning.Continue;
using SubProgWPF.Learning.Interfaces;
using SubProgWPF.Utils;
using SubProgWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SubProgWPF.Learning.AddMedia
{
    public class AddMedia : BaseMediaCreation
    {
        EpisodeCreationImplementation episodeCreationImplementation;
        YoutubeCreationImplementation youtubeCreationImplementation;
        BookCreationImplementation bookCreationImplementation;


        public void EpisodeCreationSequence(IEpisode episode, BackgroundWorker worker)
        {

            episodeCreationImplementation = new EpisodeCreationImplementation();
            int transcriptionId = episodeCreationImplementation.CreateTranscription(episode);
            episode.TranscriptionId = transcriptionId;
            episodeCreationImplementation.createEpisode(episode);
            episodeCreationImplementation.saveTheWords(episode, worker);
        }
        public void createYoutube(IYoutube youtube, BackgroundWorker worker)
        {
            youtubeCreationImplementation = new YoutubeCreationImplementation();
            int transcriptionId = youtubeCreationImplementation.CreateTranscription(youtube);
            TranscriptionServices.addMediaLocationToTranscriptionByID(transcriptionId, youtube.Link);
            youtube.TranscriptionId = transcriptionId;
            youtubeCreationImplementation.createYoutube(youtube);
            youtubeCreationImplementation.saveTheWords(youtube, worker);

        }
        public void createBook(IBook book, BackgroundWorker worker)
        {
            bookCreationImplementation = new BookCreationImplementation();

            int transcriptionId = bookCreationImplementation.CreateTranscription(book);
            book.TranscriptionId = transcriptionId;
            bookCreationImplementation.createBook(book);
            bookCreationImplementation.saveTheWords(book, worker);
        }
    }
}
