using ODT_Repository.Repository;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Quartz;

[DisallowConcurrentExecution]
public class UpdateConversationIsCloseJob : IJob
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateConversationIsCloseJob(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task Execute(IJobExecutionContext context)
    {
        try
        {
            var currentTime = DateTime.Now;
            var conversations = _unitOfWork.ConversationRepository
                .Get(c => c.IsClose == false)
                .ToList();

            foreach (var conversation in conversations)
            {
                var endTimeWithDuration = conversation.EndTime;
                if (endTimeWithDuration <= currentTime)
                {
                    conversation.IsClose = true;
                    _unitOfWork.ConversationRepository.Update(conversation);
                }
            }

            _unitOfWork.Save();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        return Task.CompletedTask;
    }

}
