import pyart
from fnlib import loop

def main():
    art = pyart.pyart()
    art.turn_axis_off()
    art.invert_yaxis()

    xs = [1000, 1400, 1070]
    ys = [250, 700, 550]
    cx = 1373.136
    cy = 321.1017
    r = 380
    
    art.line(loop(xs), loop(ys), color='k')
    art.dots(xs, ys, color='k', label_dots=True)
    art.dots([cx], [cy], color='r', label_dots=True)
    art.circle(cx, cy, r, color='b')
    art.circle(cx, cy, 1957, color='g')
    art.save('circle.png', 360)
    art.show()

if __name__ == "__main__":
    main()