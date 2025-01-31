using System;
using System.Runtime.Serialization;
using Weather.Domain.Enums;

namespace Weather.Domain.DTOs
{
    public class OperationResult<TResult>
    {
        public OperationResultType ResultType { get; protected set; }
        public OperationFailure Failure { get; private set; }
        public TResult Value { get; private set; }

        [IgnoreDataMember]
        public bool IsResultOk => ResultType == OperationResultType.Ok;

        public OperationResult() : base()
        { }

        public static OperationResult<TResult> Success(TResult result = default)
        {
            var operationResult = new OperationResult<TResult>();
            operationResult.Succeed(result);
            return operationResult;
        }

        public static OperationResult<TResult> Fail(OperationResultType failure, string reason)
        {
            var operationResult = new OperationResult<TResult>();
            operationResult.Failed(failure, reason);
            return operationResult;
        }

        private void Failed(OperationResultType failure, string reason)
        {
            ResultType = failure;
            Failure = new OperationFailure() { FailureType = failure, Reason = reason };
        }

        private void Succeed(TResult result)
        {
            if (!IsResultOk)
                throw new InvalidOperationException("This result has already failed. Do not call Succeed after calling Fail.");
            Value = result;
        }
    }

    public class OperationFailure
    {
        public OperationResultType FailureType { get; set; }
        public string Reason { get; set; }
    }
}
