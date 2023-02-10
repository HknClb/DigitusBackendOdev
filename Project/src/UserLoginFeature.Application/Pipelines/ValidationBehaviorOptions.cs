namespace UserLoginFeature.Application.Pipelines
{
    public class ValidationBehaviorOptions
    {
        private bool throwValidationErrors = true;
        private bool addValidationErrorsToList = false;
        public bool ThrowValidationErrors { get => throwValidationErrors; }
        public bool AddValidationErrorsToList { get => addValidationErrorsToList; }

        /// <summary>
        /// Default
        /// </summary>
        public void UseThrowingValidationErrorsBehavior()
        {
        }

        public void UseAddingValidationErrorsToListBehavior()
        {
            throwValidationErrors = false;
            addValidationErrorsToList = true;
        }

        public void UseBothValidationBehaviors()
        {
            throwValidationErrors = true;
            addValidationErrorsToList = true;
        }
    }
}
