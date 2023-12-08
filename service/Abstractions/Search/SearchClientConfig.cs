﻿// Copyright (c) Microsoft. All rights reserved.

using System;

#pragma warning disable IDE0130 // reduce number of "using" statements
// ReSharper disable once CheckNamespace - reduce number of "using" statements
namespace Microsoft.KernelMemory;

/// <summary>
/// Settings used by the default SearchClient
/// </summary>
public class SearchClientConfig
{
    /// <summary>
    /// Maximum number of tokens accepted by the LLM used to generate answers.
    /// The number includes the tokens used for the answer, e.g. when using
    /// GPT4-32k, set this number to 32768.
    /// </summary>
    public int MaxAskPromptSize { get; set; } = 8000;

    /// <summary>
    /// Maximum number of relevant sources to consider when generating an answer.
    /// The value is also used as the max number of results returned by SearchAsync
    /// when passing a limit less or equal to zero.
    /// </summary>
    public int MaxMatchesCount { get; set; } = 100;

    /// <summary>
    /// How many tokens to reserve for the answer generated by the LLM.
    /// E.g. if the LLM supports max 4000 tokens, and AnswerTokens is 300, then
    /// the prompt sent to LLM will contain max 3700 tokens, composed by
    /// prompt + question + grounding information retrieved from memory.
    /// </summary>
    public int AnswerTokens { get; set; } = 300;

    /// <summary>
    /// Text to return when the LLM cannot produce an answer.
    /// </summary>
    public string EmptyAnswer { get; set; } = "INFO NOT FOUND";

    /// <summary>
    /// Verify that the current state is valid.
    /// </summary>
    public void Validate()
    {
        if (this.MaxAskPromptSize < 1024)
        {
            throw new ArgumentOutOfRangeException(nameof(this.MaxAskPromptSize),
                $"{nameof(this.MaxAskPromptSize)} cannot be less than 1024");
        }

        if (this.MaxMatchesCount < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(this.MaxMatchesCount),
                $"{nameof(this.MaxMatchesCount)} cannot be less than 1");
        }

        if (this.AnswerTokens < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(this.AnswerTokens),
                $"{nameof(this.AnswerTokens)} cannot be less than 1");
        }

        if (this.EmptyAnswer.Length > 256)
        {
            throw new ArgumentOutOfRangeException(nameof(this.EmptyAnswer),
                $"{nameof(this.EmptyAnswer)} is too long, consider something shorter");
        }
    }
}
