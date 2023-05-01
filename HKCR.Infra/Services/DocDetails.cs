﻿using HKCR.Application.Common.DTO.Document;
using HKCR.Application.Common.Interface;
using HKCR.Domain.Entities;

namespace HKCR.Infra.Services;

public class DocDetails : IDocDetails
{
    private readonly IApplicationDbContext _dbContext;

    public DocDetails(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DocResponseDto> AddDocDetails(DocRequestDto doc)
    {
        var docDetails = new Document()
        {
            DocType = doc.DocType,
            DocImage = doc.DocImage
        };
        await _dbContext.Document.AddAsync(docDetails);
        await _dbContext.SaveChangesAsync(default(CancellationToken));


        var result = new DocResponseDto()
        {
            DocType = docDetails.DocType,
            DocImage = docDetails.DocImage
        };
        return result;
    }


    public async Task<List<DocResponseDto>> GetAllDocsAsync()
    {
        var data = (from docData in _dbContext.Document
            select new DocResponseDto()
            {
                DocId = docData.DocID,
                DocType = docData.DocType,
                DocImage = docData.DocImage
            }).ToList();
        return data;
    }

    public Task<List<DocResponseDto>> GetAllDocs()
    {
        throw new NotImplementedException();
    }
}