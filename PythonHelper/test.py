import os

songs_path = 'songs/'
current_song = 0
songs = []
point_size = 0
window_size = 1500

def ensure_directory():
    if not os.path.exists(songs_path):
        os.mkdir(songs_path)
    
def keyPressed():
    global current_song
    if key == 'w':
        current_song = min(len(songs) - 1, current_song + 1)
    if key == 's':
        current_song = max(0, current_song - 1)
    print('Current song: ' + str(songs[current_song].replace(".txt", '')))
    visualize_current_song()

def write_file(name, txt):
    with open(name, 'w') as fl:
        fl.writelines(txt)
        
def read_file(name):
    with open(name, 'r') as fl:
        lines = fl.readlines()
    return lines

def read_current():
    return read_file(join(songs_path, songs[current_song]))

def join(base, *path):
    return os.path.join(base, *path)
 
def read_files(dir):
    return [i for i in os.listdir(dir) if '.txt' in i]

def read_songs():
    return read_files(songs_path)

def init_songs():
    global songs
    songs = read_songs()
    
def to_words(lines):
    words = []
    for ln in lines:
        for word in ln.split():
            if word not in ['', ',', '.', '—', '-', ' ', ':', ';']:
                words.append(word.strip())
    return words
    # [[word.strip() for word in ln.split()] for ln in lines]

def draw_matrix(words):
    # cell_size = window_size // len(words)
    # print('cell size: ' + str(cell_size))
    # for i in words:
    #     for j in words:
    #         if i == j:
    #             rect

    loadPixels()
    c = color(0, 0, 0)
    # for i in range(100):
    #     for j in range(100):
    #         pixels[i * width + j] = c
    for i in range(len(words)):
        for j in range(len(words)):
            if words[i] == words[j]:
                pixels[i * width + j] = c
    updatePixels()

def clear():
    loop()
    background(255)
    #noLoop()

def visualize_current_song():
    if len(songs) == 0: 
        return
    lines = read_current()
    words = to_words(lines)
    print('Words: ' + str(len(words)))
    clear()
    draw_matrix(words)
    
def setup():
    ensure_directory()
    init_songs()
    if not songs: 
        return
    print(songs)
    size(window_size, window_size)
    clear()
    stroke(0)
    #noLoop()
    visualize_current_song()
    
def draw():
    pass
