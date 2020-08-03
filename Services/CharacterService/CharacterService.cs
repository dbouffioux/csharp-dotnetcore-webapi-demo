using System.Xml.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using test_dotnet_webapi.Dtos.Character;
using test_dotnet_webapi.Models;
using test_dotnet_webapi.Data;
using Microsoft.EntityFrameworkCore;

namespace test_dotnet_webapi.Services.CharacterService {
    public class CharacterService : ICharacterService {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService (IMapper mapper, DataContext context) {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacter () {
            ServiceResponse<List<GetCharacterDto>> response = new ServiceResponse<List<GetCharacterDto>> ();
            List<Character> dbCharacters = await _context.Characters.ToListAsync();
            response.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById (int id) {
            ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto> ();
            Character dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            response.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter (AddCharacterDto newCharacter) {
            ServiceResponse<List<GetCharacterDto>> response = new ServiceResponse<List<GetCharacterDto>> ();
            Character character = _mapper.Map<Character>(newCharacter);
            await _context.Characters.AddAsync(character);
            await _context.SaveChangesAsync();
            response.Data = _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter (UpdateCharacterDto updateCharacter) {
            ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto> ();
            try {
                Character character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updateCharacter.Id);

                character.Name = updateCharacter.Name;
                character.Class = updateCharacter.Class;
                character.Defense = updateCharacter.Defense;
                character.HitPoints = updateCharacter.HitPoints;
                character.Intelligence = updateCharacter.Intelligence;
                character.Strength = updateCharacter.Strength;
            
                _context.Characters.Update(character);
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetCharacterDto>(updateCharacter);
            } catch(Exception exception) {
                response.Success = false;
                response.Message = exception.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter (int id) {
            ServiceResponse<List<GetCharacterDto>> response = new ServiceResponse<List<GetCharacterDto>> ();
            try {
                Character character = await _context.Characters.FirstAsync(c => c.Id == id);
                _context.Characters.Remove(character);
                await _context.SaveChangesAsync();
                response.Data =  _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            } catch(Exception exception) {
                response.Success = false;
                response.Message = exception.Message;
            }
            return response;
        }
    }
}