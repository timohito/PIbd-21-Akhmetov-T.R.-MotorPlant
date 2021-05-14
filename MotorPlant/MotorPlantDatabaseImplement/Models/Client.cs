﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MotorPlantDatabaseImplement.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required]
        public string ClientFIO { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [ForeignKey("ClientId")]
        public virtual List<Order> Order { get; set; }

        [ForeignKey("ClientId")]
        public List<MessageInfo> Messages { get; set; }
    }
}
