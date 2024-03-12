using AutoMapper;
using CounTrivia.Entities;
using CounTrivia.Entities.DataTransferObjects;
using CounTrivia.Entities.Rest;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ApiResponseCountry, Country>()
            .ForMember(dest => dest.CommonName, opt => opt.MapFrom(src => src.Name.Common))
            .ForMember(dest => dest.OfficialName, opt => opt.MapFrom(src => src.Name.Official))
            .ForMember(dest => dest.Capital, opt => opt.MapFrom(src => src.Capital.FirstOrDefault()))
            .ForMember(dest => dest.Neighbours, opt => opt.MapFrom(src => src.Borders))
            .ForPath(dest => dest.Flags.PngUrl, opt => opt.MapFrom(src => src.Flags.Png))
            .ForPath(dest => dest.Flags.SvgUrl, opt => opt.MapFrom(src => src.Flags.Svg))
            .ForPath(dest => dest.Flags.Emoji, opt => opt.MapFrom(src => src.Flag))
            .ForPath(dest => dest.Flags.Description, opt => opt.MapFrom(src => src.Flags.Alt));

        CreateMap<Challenge, ChallengeResponseDTO>()
            .ForMember(dest => dest.AnswerDisplayType, opt => opt.MapFrom(src => src.AnswerOptionGenerator.AnswerDisplayType))
            .ForMember(dest => dest.AnswerOptions, opt => opt.MapFrom((src, dest) =>
            {
                List<AnswerOption> answerOptions = src.CorrectAnswers
                    .Select(solution => src.AnswerOptionGenerator.GenerateAnswerOption(solution, true))
                    .ToList();
                answerOptions.AddRange(src.AlternativeAnswers.Select(solution => src.AnswerOptionGenerator.GenerateAnswerOption(solution, false)));
                return answerOptions;
            }));

        CreateMap<AnswerOption, AnswerOptionResponseDTO>();
    }
}