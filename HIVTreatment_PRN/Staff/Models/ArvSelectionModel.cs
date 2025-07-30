using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HIVTreatment_PRN.Staff.Models
{
    public class ArvSelectionModel : INotifyPropertyChanged
    {
        private int _arvId;
        private string _arvName;
        private string _arvDescription;
        private string _dosage;
        private string _usageInstruction;
        private string _status = "Active"; // Giá trị mặc định

        public int ArvId
        {
            get => _arvId;
            set
            {
                if (_arvId != value)
                {
                    _arvId = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ArvName
        {
            get => _arvName;
            set
            {
                if (_arvName != value)
                {
                    _arvName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ArvDescription
        {
            get => _arvDescription;
            set
            {
                if (_arvDescription != value)
                {
                    _arvDescription = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Dosage
        {
            get => _dosage;
            set
            {
                if (_dosage != value)
                {
                    _dosage = value;
                    OnPropertyChanged();
                }
            }
        }

        public string UsageInstruction
        {
            get => _usageInstruction;
            set
            {
                if (_usageInstruction != value)
                {
                    _usageInstruction = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ProtocolInfoModel : INotifyPropertyChanged
    {
        private int _protocolId;
        private string _name;
        private string _description;
        private string _status = "Active"; // Giá trị mặc định
        private ObservableCollection<ArvSelectionModel> _arvSelections = new ObservableCollection<ArvSelectionModel>();

        public int ProtocolId
        {
            get => _protocolId;
            set
            {
                if (_protocolId != value)
                {
                    _protocolId = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<ArvSelectionModel> ArvSelections
        {
            get => _arvSelections;
            set
            {
                if (_arvSelections != value)
                {
                    _arvSelections = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}