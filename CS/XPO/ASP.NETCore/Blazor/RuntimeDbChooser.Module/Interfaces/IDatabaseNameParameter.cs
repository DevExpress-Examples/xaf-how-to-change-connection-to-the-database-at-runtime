using RuntimeDbChooser.Module.BusinessObjects;

namespace RuntimeDbChooser.Services;
public interface IDatabaseNameParameter {
    DataBaseNameHolder DatabaseName { get; set; }
}
