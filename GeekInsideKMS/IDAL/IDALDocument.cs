﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Models;

namespace IDAL
{
    public interface IDALDocument
    {
        List<DocumentModel> getAllCheckedByPublisherNumber(int publisherNumber);
        List<DocumentModel> getAllUncheckedByPublisherNumber(int publisherNumber);
    }
}
